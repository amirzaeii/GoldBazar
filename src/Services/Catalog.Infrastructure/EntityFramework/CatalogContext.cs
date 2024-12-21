using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
        {            
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Material> Materials { get; set; }
        public DbSet<Metal> Metals { get; set; } 
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Occassion> Occasions { get; set; } 
        public DbSet<Style> Styles { get; set; } 

    }
}
