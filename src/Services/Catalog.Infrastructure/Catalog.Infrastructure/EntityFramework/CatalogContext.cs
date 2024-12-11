using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
        {            
        }
        public DbSet<Shop> GoldSmiths { get; set; }
        public DbSet<Product> Products { get; set; } = null!;

    }
}
