using Catalog.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options, IConfiguration configuration) : base(options)
        {            
        }
        public DbSet<GoldSmith> GoldSmiths { get; set; }
    }
}
