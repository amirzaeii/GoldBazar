using Microsoft.Extensions.Configuration;
namespace Catalog.Infrastructure.EntityFramework;
public class CatalogContext : DbContext
{
        public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
        {            
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; } 
        public DbSet<Material> Materials { get; set; }
        public DbSet<Metal> Metals { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Occassion> Occasions { get; set; } 
        public DbSet<Style> Styles { get; set; } 
        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }

}

