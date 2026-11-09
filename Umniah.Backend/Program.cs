using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Umniah.Backend.Config;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;
using Umniah.Backend.Repositories;
using Umniah.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMapster();

// Local testing without Postgres. The ef tooling always gets Npgsql, so migrations stay Postgres migrations.
var useSqlite = builder.Configuration.GetValue<bool>("UseSqlite") && !EF.IsDesignTime;

builder.Services.AddDbContext<UmniahDbContext>(options =>
{
    if (useSqlite)
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    else
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBulkRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICrudService<InputGalleryImage, OutputGalleryImage>, GalleryImageService>();
builder.Services.AddScoped<ICrudService<InputProduct, OutputProduct>, ProductService>();
builder.Services.AddScoped<ICrudService<InputPurchase, OutputPurchase>, PurchaseService>();
builder.Services.AddScoped<ICrudService<InputSale, OutputSale>, SaleService>();
builder.Services.AddScoped<ICrudService<InputOrder, OutputOrder>, OrderService>();
builder.Services.AddScoped<IBulkCrudService<InputSeller, OutputSeller>, SellerService>();

builder.Services.Configure<FileLedgerOptions>(builder.Configuration.GetSection(FileLedgerOptions.SectionName));
builder.Services.AddScoped<IFileLedger, FileLedger>();
builder.Services.AddScoped<ISiteSettingsService, SiteSettingsService>();
builder.Services.AddHostedService<FileLedgerCleanupJob>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// SQLite can't run the Postgres migrations, so it builds the schema from the model.
// It never alters an existing file: delete the .db file after a model change.
if (useSqlite)
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<UmniahDbContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// In development the frontends reach the API over plain HTTP through the Vite proxy.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

var uploadsRoot = FileLedger.GetUploadsRoot(app.Environment);
Directory.CreateDirectory(uploadsRoot);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsRoot),
    RequestPath = FileLedger.UploadsRequestPath
});

// Use CORS
app.UseCors("AllowAll");

// Add authentication & authorization middleware if needed
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();