using Microsoft.EntityFrameworkCore;

namespace Umniah.Backend.Data.Context;

public class UmniahDbContext : DbContext
{
    public UmniahDbContext(DbContextOptions<UmniahDbContext> options) : base(options)
    {
    }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<GalleryImage> GalleryImages { get; set; }
    public DbSet<Seller> Sellers { get; set; }
}