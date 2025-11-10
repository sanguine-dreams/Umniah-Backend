using Mapster;
using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;
using Umniah.Backend.Repositories;
using Umniah.Backend.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddMapster();

builder.Services.AddDbContext<UmniahDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IBulkRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ICrudService<InputGalleryImage, OutputGalleryImage>, GalleryImageService>();
builder.Services.AddScoped<ICrudService<InputProduct, OutputProduct>, ProductService>();
builder.Services.AddScoped<ICrudService<InputPurchase, OutputPurchase>, PurchaseService>();
builder.Services.AddScoped<ICrudService<InputSale, OutputSale>, SaleService>();
builder.Services.AddScoped<ICrudService<InputOrder, OutputOrder>, OrderService>();
builder.Services.AddScoped<IBulkCrudService<InputSeller, OutputSeller>, SellerService>();

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