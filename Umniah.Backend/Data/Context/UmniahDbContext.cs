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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Prevent cascade deletes globally: set Restrict for all foreign keys
        foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<Product>()
        .HasOne(p => p.Purchase)
        .WithMany(pu => pu.Product)     // adjust if property name differs
        .HasForeignKey(p => p.PurchaseId)
        .OnDelete(DeleteBehavior.SetNull);
    }
}