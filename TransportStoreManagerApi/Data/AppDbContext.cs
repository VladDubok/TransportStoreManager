using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data.Entities;

namespace TransportStoreManagerApi.Data;

public class AppDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<ProductPrice> ProductsPrices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<BlobFile> Files { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductPhoto> ProductPhotos { get; set; }
    public DbSet<ProductPromotion> ProductPromotions { get; set; }
    public DbSet<ProductPromotionHistory> ProductPromotionHistories { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Entities Configuration

        // modelBuilder.Entity<Brand>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Brand>()
        //     .HasMany<Product>()
        //     .WithOne(x => x.Brand)
        //     .HasForeignKey(x => x.BrandId);
        //
        // modelBuilder.Entity<Product>()
        //     .HasKey(x => x.Id);
        
        // modelBuilder.Entity<Brand>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Brand>()
        //     .HasMany<Product>()
        //     .WithOne(x => x.Brand)
        //     .HasForeignKey(x => x.BrandId);
        //
        // modelBuilder.Entity<ProductPrice>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Customer>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Customer>()
        //     .HasMany<Product>()
        //     .WithOne(x => x.Customer)
        //     .HasForeignKey(x => x.CustomerId);
        //
        // modelBuilder.Entity<BlobFile>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<BlobFile>()
        //     .HasMany<ProductPhoto>()
        //     .WithOne(x => x.BlobFile)
        //     .HasForeignKey(x => x.FileId);
        //
        // modelBuilder.Entity<OutboxMessage>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Product>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Product>()
        //     .HasMany<ProductPhoto>()
        //     .WithOne(x => x.Product)
        //     .HasForeignKey(x => x.ProductId);
        //
        // modelBuilder.Entity<Product>()
        //     .HasMany<ProductPromotion>()
        //     .WithOne(x => x.Product)
        //     .HasForeignKey(x => x.ProductId);
        //
        // modelBuilder.Entity<Product>()
        //     .HasMany<ProductPrice>()
        //     .WithOne(x => x.Product)
        //     .HasForeignKey(x => x.ProductId);
        //
        // modelBuilder.Entity<ProductPhoto>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<ProductPromotion>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<ProductPrice>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<ProductPromotion>()
        //     .HasMany<ProductPromotionHistory>()
        //     .WithOne(x => x.ProductPromotion)
        //     .HasForeignKey(x => x.ProductPromotionId);
        //
        // modelBuilder.Entity<ProductPromotionHistory>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<ProductType>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<ProductType>()
        //     .HasMany<Product>()
        //     .WithOne(x => x.ProductType)
        //     .HasForeignKey(x => x.ProductTypeId);
        //
        // modelBuilder.Entity<Promotion>()
        //     .HasKey(x => x.Id);
        //
        // modelBuilder.Entity<Promotion>()
        //     .HasMany<ProductPromotion>()
        //     .WithOne(x => x.Promotion)
        //     .HasForeignKey(x => x.PromotionId);

        #endregion

        #region Data Seed
        
        modelBuilder.Entity<ProductType>()
            .HasData(
                new ProductType { Id = 1, Name = "Car" },
                new ProductType { Id = 2, Name = "Motorcycle" });

        #endregion
    }
}