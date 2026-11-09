using Microsoft.EntityFrameworkCore;

namespace Umniah.Backend.Data.Context;

public class UmniahDbContext(DbContextOptions<UmniahDbContext> options) : DbContext(options)
{
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<GalleryImage> GalleryImages { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<StoredFile> StoredFiles { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<GalleryThemeText> GalleryThemeTexts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Prevent cascade deletes globally
        foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Entity<Product>()
        .HasOne(p => p.Purchase)
        .WithMany(pu => pu.Product)    
        .HasForeignKey(p => p.PurchaseId)
        .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<StoredFile>(entity =>
        {
            entity.Property(f => f.FileName).HasMaxLength(64);
            entity.Property(f => f.LastError).HasMaxLength(400);
            entity.HasIndex(f => f.FileName).IsUnique();
            entity.HasIndex(f => new { f.Status, f.CreatedOn });
        });

        modelBuilder.Entity<SiteSetting>(entity =>
        {
            entity.Property(s => s.BannerTextEn).HasMaxLength(SiteSetting.MaxTextLength);
            entity.Property(s => s.BannerTextAr).HasMaxLength(SiteSetting.MaxTextLength);
        });

        modelBuilder.Entity<GalleryThemeText>(entity =>
        {
            entity.HasKey(t => t.Theme);
            entity.Property(t => t.Theme).ValueGeneratedNever();
            entity.Property(t => t.HeroTextEn).HasMaxLength(GalleryThemeText.MaxTextLength);
            entity.Property(t => t.HeroTextAr).HasMaxLength(GalleryThemeText.MaxTextLength);
        });
    }
}
