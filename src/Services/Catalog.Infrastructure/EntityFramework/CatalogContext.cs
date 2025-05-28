using Catalog.Infrastructure.EntityConfigurations;

using Microsoft.Extensions.Configuration;
namespace Catalog.Infrastructure.EntityFramework;

public class CatalogContext : DbContext
{
        public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
        {
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Metal> Metals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Occassion> Occasions { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
        public DbSet<PromotionSlider> PromotionSlider { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                EntityConfiguration.CategoryConfigure(modelBuilder);
                EntityConfiguration.CityConfigure(modelBuilder);
                EntityConfiguration.GovernorateConfigure(modelBuilder);
                EntityConfiguration.ItemConfigure(modelBuilder);
                EntityConfiguration.ItemPhotoConfigure(modelBuilder);
                EntityConfiguration.MaterialConfigure(modelBuilder);
                EntityConfiguration.MetalConfigure(modelBuilder);
                EntityConfiguration.OccassionConfigure(modelBuilder);
                EntityConfiguration.StyleConfigure(modelBuilder);
                EntityConfiguration.ManufactureConfigure(modelBuilder);
                EntityConfiguration.ShopConfigure(modelBuilder);
                EntityConfiguration.SliderConfigure(modelBuilder);

        }

}


