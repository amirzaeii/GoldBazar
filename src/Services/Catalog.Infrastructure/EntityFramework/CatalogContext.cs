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
        public DbSet<Item> Items { get; set; } 
        public DbSet<Material> Materials { get; set; }
        public DbSet<Metal> Metals { get; set; } 
        public DbSet<Type> Types { get; set; }
        public DbSet<Occassion> Occasions { get; set; } 
        public DbSet<Style> Styles { get; set; } 

    }
}
