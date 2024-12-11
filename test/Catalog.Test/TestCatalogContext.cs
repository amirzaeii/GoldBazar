using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Test
{
    public class TestCatalogContext : DbContext
    {
        public TestCatalogContext(DbContextOptions<TestCatalogContext> options) : base(options) { }

        public DbSet<GoldSmith> GoldSmiths { get; set; }
    }
}
