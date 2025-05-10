using System.Text.Json;
using Npgsql;

namespace Catalog.Infrastructure.EntityFramework;
public partial class ContextData {
    public Category[] Categories { get; set; }
    public Material[] Materials { get; set; }
    public Metal[] Metals { get; set; }
    public Manufacture[] Manufactures { get; set; }
    public Item[] Items { get; set; }
    public Occassion[] Occasions { get; set; }
    public Style[] Styles { get; set; }
    public Shop[] Shops { get; set; }
    public ContextData()
    {
        Categories = [ 
                new Category{Id = 1, Name = "Set", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/FullSet.png"},
                new Category{Id = 2, Name = "Half-Set", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/HalfSet.png"}, 
                new Category{Id = 3, Name = "Belt", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Belt.png"},
                new Category{Id = 4, Name = "Belt-Accessory", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/BeltPendant.png"}, 
                new Category{Id = 5, Name = "Bracelet", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/BraceletSingle.png"}, 
                new Category{Id = 6, Name = "Bracelet(2)", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Bracelet2.png"}, 
                new Category{Id = 7, Name = "Bracelet(6)", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Bracelet6.png"}, 
                new Category{Id = 8, Name = "Bracelet(12)", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Bracelet12.png"},                 
                new Category{Id = 9, Name = "Ring", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Ring.png"}, 
                new Category{Id = 10, Name = "Wedding-Ring", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/WeddingBand.png"}, 
                new Category{Id = 11, Name = "Neck-piece", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Belt.png"}, 
                new Category{Id = 12, Name = "Baraq", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Baraq.png"}, 
                new Category{Id = 13, Name = "Shal", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Shal.png"},
                //new Category{Id = 14, Name = "Chest-Piece", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Belt.png"},
                new Category{Id = 15, Name = "Necklace", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Necklace.png"},
                new Category{Id = 16, Name = "Tiara", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Tiara.png"}, 
                new Category{Id = 17, Name = "Earring", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Earrings.png"},
                new Category{Id = 18, Name = "Ashqband", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Ashqband.png"},
                new Category{Id = 19, Name = "Dastband", Photo = "https://goldbazarblob.blob.core.windows.net/images/category/Dastband.png"},
            ];

        Materials = [new Material{Id = 1, Name = "Gold"},
                new Material{Id = 2, Name = "Silver"}, 
                new Material{Id = 3, Name = "Diamond"},
                new Material{Id = 4, Name = "Platinum"}, 
                new Material{Id = 5, Name = "Copper"}, 
                new Material{Id = 6, Name = "Brass"}, 
                new Material{Id = 7, Name = "Bronze"}, 
                new Material{Id = 8, Name = "Steel"}];
                
        Metals = [new Metal{Id = 1, Name = "Yellow-Gold", MaterialId = 1},
                new Metal{Id = 2, Name = "Rose-Gold", MaterialId = 1},
                new Metal{Id = 3, Name = "White-Gold", MaterialId = 1}];

        Manufactures = [ 
                new Manufacture{Id = 1, Karat = 18, Purity = 0.755M, Name = "DUBAI-18KT"},
                new Manufacture{Id = 2, Karat = 21, Purity = 0.880M, Name = "DUBAI-21KT"},
                new Manufacture{Id = 3, Karat = 22, Purity = 0.920M, Name = "DUBAI-22KT"},
                new Manufacture{Id = 4, Karat = 24, Purity = 0.1M, Name = "DUBAI-24KT"},                
                new Manufacture{Id = 5, Karat = 18, Purity = 0.750M, Name = "TURKEY-18KT"},
                new Manufacture{Id = 6, Karat = 21, Purity = 0.875M, Name = "TURKEY-21KT"},
                new Manufacture{Id = 7, Karat = 22, Purity = 0.916M, Name = "TURKEY-22KT"},
                new Manufacture{Id = 8, Karat = 24, Purity = 0.1M, Name = "TURKEY-24KT"},                
                new Manufacture{Id = 9, Karat = 18, Purity = 0.750M, Name = "IRAQ-18KT"},
                new Manufacture{Id = 10, Karat = 21, Purity = 0.875M, Name = "IRAQ-21KT"},
                new Manufacture{Id = 11, Karat = 22, Purity = 0.916M, Name = "IRAQ-22KT"},
                new Manufacture{Id = 12, Karat = 24, Purity = 0.1M, Name = "IRAQ-24KT"}];

        Items = new Item[0];

        Occasions = [new Occassion{Id = 1, Name = "Wedding"},
                new Occassion{Id = 2, Name = "Engagement"}, 
                new Occassion{Id = 3, Name = "Anniversary"},
                new Occassion{Id = 4, Name = "Birthday"}, 
                new Occassion{Id = 5, Name = "Festival"}, 
                new Occassion{Id = 6, Name = "Casual"}, 
                new Occassion{Id = 7, Name = "Formal"},
                new Occassion{Id = 8, Name = "Party"},
                new Occassion{Id = 9, Name = "Gift"},
                new Occassion{Id = 10, Name = "Everyday"},
                new Occassion{Id = 11, Name = "Special Occasion"}];

        Styles = [new Style{Id = 1, Name = "Traditional"},
                new Style{Id = 2, Name = "Modern"}, 
                new Style{Id = 3, Name = "Antique"},
                new Style{Id = 4, Name = "Contemporary"}, 
                new Style{Id = 5, Name = "Classic"}, 
                new Style{Id = 6, Name = "Trendy"}, 
                new Style{Id = 7, Name = "Minimalist"},
                new Style{Id = 8, Name = "Bohemian"},
                new Style{Id = 9, Name = "Vintage"},
                new Style{Id = 10, Name = "Art Deco"},
                new Style{Id = 11, Name = "Gothic"}];

        Shops = new Shop[0];
    }
}
public partial class CatalogDevelopmentContextSeed(
    //IWebHostEnvironment env,
    //ICatalogAI catalogAI,
    ILogger<CatalogDevelopmentContextSeed> logger) : IDbSeeder<CatalogContext>
{
    public async Task SeedAsync(CatalogContext context)
    {
        var contextData = new ContextData();

        // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
        context.Database.OpenConnection();
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

        if(!context.Shops.Any()){
           
            await context.Shops.AddRangeAsync(contextData.Shops);
            logger.LogInformation("Seeded shops with {NumItems} items", context.Shops.Count());
            await context.SaveChangesAsync();
        }      
        if(!context.Items.Any()){
           
            await context.Items.AddRangeAsync(contextData.Items);
            logger.LogInformation("Seeded items with {NumItems} items", context.Items.Count());
            await context.SaveChangesAsync();
        }      

        if (!context.Materials.Any())
        {
            await context.Materials.AddRangeAsync(contextData.Materials);

             logger.LogInformation("Seeded catalog with {NumBrands} Material", context.Materials.Count());
             await context.SaveChangesAsync();
             
        }
        if (!context.Metals.Any())
        {
            await context.Metals.AddRangeAsync(contextData.Metals);
            
            logger.LogInformation("Seeded catalog with {NumBrands} Metals", context.Metals.Count());
            await context.SaveChangesAsync();

        }
        if(!context.Manufactures.Any())
        {
            await context.Manufactures.AddRangeAsync(contextData.Manufactures);

             logger.LogInformation("Seeded catalog with {NumBrands} Manufactures", context.Manufactures.Count());
             await context.SaveChangesAsync();
        }
        if (!context.Styles.Any())
        {
            await context.Styles.AddRangeAsync(contextData.Styles);

             logger.LogInformation("Seeded catalog with {NumBrands} Styles", context.Styles.Count());
             await context.SaveChangesAsync();
        }
        if(!context.Occasions.Any())
        {
            await context.Occasions.AddRangeAsync(contextData.Occasions);

             logger.LogInformation("Seeded catalog with {NumBrands} Styles", context.Styles.Count());
             await context.SaveChangesAsync();
        }
        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(contextData.Categories);

             logger.LogInformation("Seeded catalog with {NumBrands} Categories", context.Categories.Count());
             await context.SaveChangesAsync();
        }
    }
}
public partial class CatalogContextSeed(
    ILogger<CatalogContextSeed> logger) : IDbSeeder<CatalogContext>
{
    public async Task SeedAsync(CatalogContext context)
    {
        var contextData = new ContextData();
        // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
        context.Database.OpenConnection();
        ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypes();

         if (!context.Materials.Any())
        {
            await context.Materials.AddRangeAsync(contextData.Materials);

             logger.LogInformation("Seeded catalog with {NumBrands} Material", context.Materials.Count());
             await context.SaveChangesAsync();
             
        }
        if (!context.Metals.Any())
        {
            await context.Metals.AddRangeAsync(contextData.Metals);
            
            logger.LogInformation("Seeded catalog with {NumBrands} Metals", context.Metals.Count());
            await context.SaveChangesAsync();

        }
        if(!context.Manufactures.Any())
        {
            await context.Manufactures.AddRangeAsync(contextData.Manufactures);

             logger.LogInformation("Seeded catalog with {NumBrands} Manufactures", context.Manufactures.Count());
             await context.SaveChangesAsync();
        }
        if (!context.Styles.Any())
        {
            await context.Styles.AddRangeAsync(contextData.Styles);

             logger.LogInformation("Seeded catalog with {NumBrands} Styles", context.Styles.Count());
             await context.SaveChangesAsync();
        }
        if(!context.Occasions.Any())
        {
            await context.Occasions.AddRangeAsync(contextData.Occasions);

             logger.LogInformation("Seeded catalog with {NumBrands} Styles", context.Styles.Count());
             await context.SaveChangesAsync();
        }
        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(contextData.Categories);

             logger.LogInformation("Seeded catalog with {NumBrands} ProductTypes", context.Categories.Count());
             await context.SaveChangesAsync();
        }
    }
}

