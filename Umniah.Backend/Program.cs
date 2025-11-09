using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;
using Umniah.Backend.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext - move this before builder.Build()
builder.Services.AddDbContext<UmniahDbContext>(
    options => options.UseNpgsql(builder.Configuration["ConnectionStrings:DefaultConnectionString"]),
    ServiceLifetime.Scoped);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Register Services
builder.Services.AddScoped<ICrudService<InputGalleryImage, OutputGalleryImage>, GalleryImageService>();
builder.Services.AddScoped<ICrudService<InputProduct, OutputProduct>, ProductService>();
builder.Services.AddScoped<ICrudService<InputPurchase, OutputPurchase>, PurchaseService>();
builder.Services.AddScoped<ICrudService<InputSale, OutputSale>, SaleService>();
builder.Services.AddScoped<ICrudService<InputOrder, OutputOrder>, OrderService>();
builder.Services.AddScoped<IBulkCrudService<InputSeller, OutputSeller>, SellerService>();

// Add CORS
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

// Add authentication & authorization middleware if needed
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();