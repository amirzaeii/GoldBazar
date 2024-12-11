using AutoMapper;
using Catalog.Api.Entities;
using Catalog.Api.Profiles;
using GoldBazar.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Test
{
    public class AutoMapperIntegrationTests
    {
        private readonly IMapper _mapper;

        public AutoMapperIntegrationTests()
        {
            // Initialize AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetAllGoldSmiths_ReturnsMappedData()
        {
            // Arrange: Setup in-memory database
            var options = new DbContextOptionsBuilder<TestCatalogContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using (var context = new TestCatalogContext(options))
            {
                // Seed data
                context.GoldSmiths.AddRange(
                    new GoldSmith
                    {
                        Id = 1,
                        Name = "Goldsmith A",
                        City = "City A",
                        Address = "123 Main St",
                        ContactNumber = "123456789",
                        Rating = 4.5f
                    },
                    new GoldSmith
                    {
                        Id = 2,
                        Name = "Goldsmith B",
                        City = "City B",
                        Address = "456 Elm St",
                        ContactNumber = "987654321",
                        Rating = 4.8f
                    }
                );
                await context.SaveChangesAsync();
            }

            using (var context = new TestCatalogContext(options))
            {
                // Act: Fetch data
                var goldsmiths = await context.GoldSmiths.ToListAsync();
                var result = _mapper.Map<List<GoldSmithViewModel>>(goldsmiths);

                // Assert: Verify the mappings
                Assert.IsNotNull(result); // Use Assert.NotNull
                Assert.Equals(2, result.Count); // Use Assert.Equal

                Assert.Equals("Goldsmith A", result[0].Name);
                Assert.Equals("Goldsmith B", result[1].Name);
                Assert.Equals("City A", result[0].City);
                Assert.Equals("City B", result[1].City);
            }
        }
    }
}
