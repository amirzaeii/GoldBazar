using System.Reflection;
using System.Text.Json;
using Npgsql;

namespace Catalog.Infrastructure;

public partial class CatalogContextSeed(
    //IWebHostEnvironment env,
    //ICatalogAI catalogAI,
    ILogger<CatalogContextSeed> logger) : IDbSeeder<CatalogContext>
{
    public async Task SeedAsync(CatalogContext context)
    {
        var contentRootPath = "";//env.ContentRootPath;

        // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
        context.Database.OpenConnection();
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

        if(!context.Shops.Any()){
            var sourcePath = Path.Combine(contentRootPath, "Setup", "shopsSampleData.json");
            var sourceJson = File.ReadAllText(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<ShaopSourceEntry[]>(sourceJson);
            var shopItems = sourceItems.Select(source => new Shop
                        {
                            Id = source.shopId,
                            Name = source.name,
                            Owner = source.owner,
                            ContactNumber = source.phoneNumber,
                            City = "Sulaymaniyah",
                            Address = "Sulaymaniyah Bazar"
                        }).ToArray();

                        await context.Shops.AddRangeAsync(shopItems);
                        logger.LogInformation("Seeded shops with {NumItems} items", context.Shops.Count());
                        await context.SaveChangesAsync();
        }

        
        if (!context.Types.Any())
        {
            var sourcePath = Path.Combine(contentRootPath, "Setup", "catalogSampleData.json");
            var sourceJson = File.ReadAllText(sourcePath);
            var sourceItems = JsonSerializer.Deserialize<CatalogSourceEntry[]>(sourceJson);

            context.Types.RemoveRange(context.Types);
            await context.Types.AddRangeAsync(sourceItems.Select(x => x.productType).Distinct()
                .Select(brandName => new Type { Name = brandName }));

            logger.LogInformation("Seeded catalog with {NumBrands} ProductTypes", context.Types.Count());

            context.Materials.RemoveRange(context.Materials);
            await context.Materials.AddRangeAsync(sourceItems.Select(x => x.material).Distinct()
                .Select(materialName => new Material { Name = materialName }));
            logger.LogInformation("Seeded catalog with {NumTypes} types", context.Materials.Count());

            context.Metals.RemoveRange(context.Metals);
            await context.Metals.AddRangeAsync(sourceItems.Select(x => x.metal).Distinct()
                .Select(metalName => new Metal { Name = metalName }));
            logger.LogInformation("Seeded catalog with {NumTypes} Metals", context.Materials.Count());

            context.Styles.RemoveRange(context.Styles);
            await context.Styles.AddRangeAsync(sourceItems.Select(x => x.style).Distinct()
                .Select(styleName => new Style { Name = styleName }));
            logger.LogInformation("Seeded catalog with {NumTypes} Styles", context.Styles.Count());


            context.Occasions.RemoveRange(context.Occasions);
            await context.Occasions.AddRangeAsync(sourceItems.Select(x => x.occasion).Distinct()
                .Select(occasionName => new Occassion { Name = occasionName }));
            logger.LogInformation("Seeded catalog with {NumTypes} occasion", context.Styles.Count());

            await context.SaveChangesAsync();

            var productTypeIdsByName = await context.Types.ToDictionaryAsync(x => x.Name, x => x.Id);
            var materialIdsByName = await context.Materials.ToDictionaryAsync(x => x.Name, x => x.Id);
            var metalIdsByName = await context.Metals.ToDictionaryAsync(x => x.Name, x => x.Id);
            var styleIdsByName = await context.Styles.ToDictionaryAsync(x => x.Name, x => x.Id);
            var occasionIdsByName = await context.Occasions.ToDictionaryAsync(x => x.Name, x => x.Id);

            var catalogItems = sourceItems.Select(source => new Item
            {
                //Id = source.Id,
                Description = source.description,
                CostPerGram = source.costPerGram,
                TypeId = productTypeIdsByName[source.productType],
                MetalId = metalIdsByName[source.metal],
                MaterialId = materialIdsByName[source.material],
                StyleId = styleIdsByName[source.style],
                OccasionId = occasionIdsByName[source.occasion],
                Size = source.size,
                Weight = source.weight,
                ShopId = source.shopId,
                 //= $"{source.Id}.webp",
            }).ToArray();

            await context.Items.AddRangeAsync(catalogItems);
            logger.LogInformation("Seeded catalog with {NumItems} items", context.Items.Count());
            await context.SaveChangesAsync();
        } 

}
    

    private class CatalogSourceEntry
    {
        public int Id { get; set; }
        public decimal weight { get; set; }
        public int size { get; set; }
        public string productType { get; set; }
        public string material { get; set; }
        public string metal { get; set; }
        public string style { get; set; }
        public string occasion { get; set; }
        public string description { get; set; }
        public decimal costPerGram { get; set; }
        public int shopId { get; set; }
    }

    private class ShaopSourceEntry
    {
        public int shopId { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string phoneNumber { get; set; }

    }
}
