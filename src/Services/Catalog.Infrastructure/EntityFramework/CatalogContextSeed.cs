using Npgsql;

namespace Catalog.Infrastructure.EntityFramework;

public partial class ContextData
{
    public Category[] Categories { get; set; }
    public Material[] Materials { get; set; }
    public Metal[] Metals { get; set; }
    public Manufacture[] Manufactures { get; set; }
    public Item[] Items { get; set; }
    public Occassion[] Occasions { get; set; }
    public Style[] Styles { get; set; }
    public Shop[] Shops { get; set; }
    public City[] Cities { get; set; }
    public Governorate[] Governorates { get; set; }
    public ContextData()
    {
        Governorates = [
            new Governorate { Id = 1, Name = "Al-Anbar" },
            new Governorate { Id = 2, Name = "Al-Basrah" },
            new Governorate { Id = 3, Name = "Al-Muthanna" },
            new Governorate { Id = 4, Name = "Al-Najaf" },
            new Governorate { Id = 5, Name = "Erbil (Hawler)" },
            new Governorate { Id = 6, Name = "As-Sulaymaniyah" },
            new Governorate { Id = 7, Name = "Babylon (Babil)" },
            new Governorate { Id = 8, Name = "Baghdad" },
            new Governorate { Id = 9, Name = "Dhi Qar" },
            new Governorate { Id = 10, Name = "Diyala" },
            new Governorate { Id = 11, Name = "Dohuk (Dahuk)" },
            new Governorate { Id = 12, Name = "Karbala" },
            new Governorate { Id = 13, Name = "Kirkuk" },
            new Governorate { Id = 14, Name = "Maysan" },
            new Governorate { Id = 15, Name = "Nineveh (Ninawa)" },
            new Governorate { Id = 16, Name = "Al-Qadisiyyah" },
            new Governorate { Id = 17, Name = "Salah ad Din" },
            new Governorate { Id = 18, Name = "Wasit" },
            new Governorate { Id = 19, Name = "Halabja" }];

        Cities = [
            new City { Id = 1, Name = "Sulaymaniyah", GovernorateId = 6 },
            new City { Id = 3, Name = "Penjwen" , GovernorateId = 6 },
            new City { Id = 4, Name = "Ranya" , GovernorateId = 6 },
            new City { Id = 5, Name = "Chamchamal" , GovernorateId = 6 },
            new City { Id = 6, Name = "Kalar" , GovernorateId = 6 },
            new City { Id = 7, Name = "Darbandikhan" , GovernorateId = 6 },
            new City { Id = 8, Name = "Dokan" , GovernorateId = 6 },
            new City { Id = 9, Name = "Said Sadiq" , GovernorateId = 6 },
            new City { Id = 10, Name = "Qaladiza" , GovernorateId = 6 },
        ];

        Categories = [
                new Category{Id = 1, Name = "Set", Photo = $"https://goldbazarblob.blob.core.windows.net/development/category/FullSet.png"},
                new Category{Id = 2, Name = "Half-Set", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/HalfSet.png"},
                new Category{Id = 3, Name = "Belt", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Belt.png"},
                new Category{Id = 4, Name = "Belt-Accessory", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/BeltPendant.png"},
                new Category{Id = 5, Name = "Bracelet", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/BraceletSingle.png"},
                new Category{Id = 6, Name = "Bracelet(2)", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Bracelet2.png"},
                new Category{Id = 7, Name = "Bracelet(6)", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Bracelet6.png"},
                new Category{Id = 8, Name = "Bracelet(12)", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Bracelet12.png"},
                new Category{Id = 9, Name = "Ring", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Ring.png"},
                new Category{Id = 10, Name = "Wedding-Ring", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/WeddingBand.png"},
                new Category{Id = 11, Name = "Neck-piece", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Belt.png"},
                new Category{Id = 12, Name = "Baraq", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Baraq.png"},
                new Category{Id = 13, Name = "Shal", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Shal.png"},
                //new Category{Id = 14, Name = "Chest-Piece", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Belt.png"},
                new Category{Id = 15, Name = "Necklace", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Necklace.png"},
                new Category{Id = 16, Name = "Tiara", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Tiara.png"},
                new Category{Id = 17, Name = "Earring", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Earrings.png"},
                new Category{Id = 18, Name = "Ashqband", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Ashqband.png"},
                new Category{Id = 19, Name = "Dastband", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/Dastband.png"},
                new Category{Id = 20, Name = "Pendant", Photo = "https://goldbazarblob.blob.core.windows.net/development/category/pendant.png"}
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

        Shops = [
                new Shop{Id = 1,
                Name = "GoldBazar Jewerly",
                Owner = "GoldBazar",
                CityId = 1,
                Address = "Sulaymanyah",
                ContactNumber = "077000000",
                Instagram = "",
                Tiktok = "",
                Description = "this is a sample shop",
                Logo = "https://goldbazarblob.blob.core.windows.net/development/shops/goldbazarshop.png",
                Banner = "https://goldbazarblob.blob.core.windows.net/development/shops/goldbazarbanner.png",},
        ];

        Items = new Item[]
{
    new Item
    {
        Id = 1,
        Caption = "Elegant Gold Necklace",
        Status = true,
        Discount = 5M,
        EligibleChangePriceRang = 5M,
        Weight = 20.1M,
        Type = Item.TypeEnum.Women,
        CategoryId = 15,       // Necklace
        MaterialId = 1,        // Gold
        MetalId = 1,           // Yellow-Gold
        OccasionId = 1,        // Wedding
        StyleId = 2,           // Modern
        Description = "18KT yellow-gold necklace with a classic modern twist.",
        CostPerGram = 12.0M,
        ShopId = 1,
        ManufactureId = 1,     // DUBAI-18KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item1.jpg",
        AvailableStock = 8,
        RestockThreshold = 2,
        MaxStockThreshold = 20
    },
    new Item
    {
        Id = 2,
        Caption = "Classic Diamond Ring",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 2M,
        Weight = 5.5M,
        Type = Item.TypeEnum.Women,
        CategoryId = 10,       // Wedding-Ring
        MaterialId = 1,        // Diamond
        MetalId = 3,           // White-Gold
        OccasionId = 2,        // Engagement
        StyleId = 5,           // Classic
        Description = "5.5g diamond ring in 21KT white-gold setting.",
        CostPerGram = 8.0M,
        ShopId = 1,
        ManufactureId = 6,     // TURKEY-21KT
        Size = 6,              // US ring size
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item2.jpg",
        AvailableStock = 3,
        RestockThreshold = 1,
        MaxStockThreshold = 10
    },
    new Item
    {
        Id = 3,
        Caption = "Men’s Gold Bracelet",
        Status = true,
        Discount = 0.00M,
        EligibleChangePriceRang = 3M,
        Weight = 25.0M,
        Type = Item.TypeEnum.Men,
        CategoryId = 5,        // Bracelet
        MaterialId = 1,        // Gold
        MetalId = 2,           // Rose-Gold
        OccasionId = 7,        // Formal
        StyleId = 6,           // Trendy
        Description = "Bold 18KT rose-gold men’s bracelet, 25g.",
        CostPerGram = 10.0M,
        ShopId = 1,
        ManufactureId = 2,     // DUBAI-21KT
        Size = 22,             // wrist circumference in cm
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item3.jpg",
        AvailableStock = 12,
        RestockThreshold = 4,
        MaxStockThreshold = 25
    },
    new Item
    {
        Id = 4,
        Caption = "Kids Gold Pendant",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 1M,
        Weight = 3.0M,
        Type = Item.TypeEnum.Kids,
        CategoryId = 20,        // pendant
        MaterialId = 1,        // Gold
        MetalId = 1,           // Yellow-Gold
        OccasionId = 10,       // Everyday
        StyleId = 7,           // Minimalist
        Description = "Lightweight gold pendant for kids, hypoallergenic.",
        CostPerGram = 5M,
        ShopId = 1,
        ManufactureId = 9,
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item4.jpg",
        AvailableStock = 20,
        RestockThreshold = 5,
        MaxStockThreshold = 50
    },
    new Item
    {
        Id = 5,
        Caption = "Art Deco Gold Bangle",
        Status = true,
        Discount = 2M,
        EligibleChangePriceRang = 4M,
        Weight = 30.0M,
        Type = Item.TypeEnum.Women,
        CategoryId = 6,        // Bracelet(2)
        MaterialId = 1,        // Gold
        MetalId = 1,           // Yellow-Gold
        OccasionId = 11,       // Special Occasion
        StyleId = 10,          // Art Deco
        Description = "30g yellow-gold bangle in art-deco design.",
        CostPerGram = 10.0M,
        ShopId = 1,
        ManufactureId = 2,     // DUBAI-21KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item5.jpg",
        AvailableStock = 0,
        RestockThreshold = 2,
        MaxStockThreshold = 15
    },
        new Item
        {
            Id = 6,
            Caption = "Royal Gold Party Set",
            Status = true,
            Discount = 1M,
            EligibleChangePriceRang = 6M,
            Weight = 51.30M,
            Type = Item.TypeEnum.Women,
            CategoryId = 1,       // Set
            MaterialId = 1,       // Gold
            MetalId = 1,          // Yellow-Gold
            OccasionId = 8,       // Party
            StyleId = 9,          // Vintage
            Description = "50g yellow-gold full set with vintage filigree work.",
            CostPerGram = 9M,
            ShopId = 1,
            ManufactureId = 2,    // DUBAI-21KT
            Size = 0,
            MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item6.jpg",
            AvailableStock = 5,
            RestockThreshold = 1,
            MaxStockThreshold = 10
        },
    new Item
    {
        Id = 7,
        Caption = "Elegant Rose-Gold Half-Set",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 5M,
        Weight = 15.21M,
        Type = Item.TypeEnum.Women,
        CategoryId = 2,       // Half-Set
        MaterialId = 1,       // Gold
        MetalId = 2,          // Rose-Gold
        OccasionId = 3,       // Anniversary
        StyleId = 4,          // Contemporary
        Description = "15g rose-gold half-set with matching earrings and pendant.",
        CostPerGram = 6M,
        ShopId = 1,
        ManufactureId = 8,    // TURKEY-24KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item7.jpg",
        AvailableStock = 7,
        RestockThreshold = 2,
        MaxStockThreshold = 15
    },


    new Item
    {
        Id = 8,
        Caption = "Traditional White-Gold Belt",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 10M,
        Weight = 83.6M,
        Type = Item.TypeEnum.Women,
        CategoryId = 3,       // Belt
        MaterialId = 1,       // Gold
        MetalId = 3,          // White-Gold
        OccasionId = 7,       // Formal
        StyleId = 1,          // Traditional
        Description = "80g white-gold bridal belt in classic traditional design.",
        CostPerGram = 10.0M,
        ShopId = 1,
        ManufactureId = 9,    // IRAQ-18KT
        Size = 100,           // belt length in cm
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item8.jpg",
        AvailableStock = 2,
        RestockThreshold = 1,
        MaxStockThreshold = 5
    },
    new Item
    {
        Id = 9,
        Caption = "Diamond Stud Earrings",
        Status = true,
        Discount = 12M,
        EligibleChangePriceRang = 0M,
        Weight = 2.53M,
        Type = Item.TypeEnum.Women,
        CategoryId = 17,      // Earring
        MaterialId = 1,       // Diamond
        MetalId = 1,          // Yellow-Gold
        OccasionId = 9,       // Gift
        StyleId = 5,          // Classic
        Description = "2.5g diamond studs set in 21KT Yellow-gold.",
        CostPerGram = 12.0M,
        ShopId = 1,
        ManufactureId = 6,    // TURKEY-21KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item9.jpg",
        AvailableStock = 15,
        RestockThreshold = 5,
        MaxStockThreshold = 30
    },

    new Item
    {
        Id = 10,
        Caption = "Gold Art-Deco Tiara",
        Status = true,
        Discount = 2M,
        EligibleChangePriceRang = 8M,
        Weight = 31.2M,
        Type = Item.TypeEnum.Women,
        CategoryId = 16,      // Tiara
        MaterialId = 1,       // Gold
        MetalId = 1,          // Yellow-Gold
        OccasionId = 1,       // Wedding
        StyleId = 11,         // Gothic
        Description = "30g gold tiara in bold art-deco style.",
        CostPerGram = 15.0M,
        ShopId = 1,
        ManufactureId = 12,   // IRAQ-24KT (placeholder)
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item10.jpg",
        AvailableStock = 1,
        RestockThreshold = 0,
        MaxStockThreshold = 2
    },
  new Item
    {
        Id = 11,
        Caption = "Gold Bohemian Bangle",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 2M,
        Weight = 18.60M,
        Type = Item.TypeEnum.Women,
        CategoryId = 7,       // Bracelet(6)
        MaterialId = 1,        // Gold
        MetalId = 2,           // Rose-Gold
        OccasionId = 6,        // Casual
        StyleId = 8,           // Bohemian
        Description = "18g rose-gold bangle with bohemian bead accents.",
        CostPerGram = 7.6M,
        ShopId = 1,
        ManufactureId = 5,     // TURKEY-18KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item11.jpg",
        AvailableStock = 10,
        RestockThreshold = 3,
        MaxStockThreshold = 20
    },

    // 12. Gold Layered Antique Necklace
    new Item
    {
        Id = 12,
        Caption = "Gold Layered Antique Necklace",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 5M,
        Weight = 12.4M,
        Type = Item.TypeEnum.Women,
        CategoryId = 11,      // Neck-piece
        MaterialId = 1,
        MetalId = 1,          // Yellow-Gold
        OccasionId = 5,       // Festival
        StyleId = 3,          // Antique
        Description = "12g layered yellow-gold necklace with antique coin pendants.",
        CostPerGram = 7M,
        ShopId = 1,
        ManufactureId = 4,    // DUBAI-22KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item12.jpg",
        AvailableStock = 6,
        RestockThreshold = 2,
        MaxStockThreshold = 15
    },
    new Item
    {
        Id = 13,
        Caption = "Gold Shal",
        Status = true,
        Discount = 2M,
        EligibleChangePriceRang = 3M,
        Weight = 2.9M,
        Type = Item.TypeEnum.Kids,
        CategoryId = 13,      // Shal
        MaterialId = 1,
        MetalId = 1,          // Yellow-Gold
        OccasionId = 9,       // Gift
        StyleId = 7,          // Minimalist
        Description = "2g yellow-gold talisman charm on a shal base.",
        CostPerGram = 11M,
        ShopId = 1,
        ManufactureId = 1,    // DUBAI-18KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item13.jpg",
        AvailableStock = 30,
        RestockThreshold = 5,
        MaxStockThreshold = 50
    },


    new Item
    {
        Id = 14,
        Caption = "Gold Baraq",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 1M,
        Weight = 10.60M,
        Type = Item.TypeEnum.Women,
        CategoryId = 12,      // Baraq
        MaterialId = 1,
        MetalId = 3,          // White-Gold
        OccasionId = 4,       // Birthday
        StyleId = 2,          // Modern
        Description = "10g white-gold baraq with sleek lines.",
        CostPerGram = 7M,
        ShopId = 1,
        ManufactureId = 10,   // IRAQ-21KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item14.jpg",
        AvailableStock = 7,
        RestockThreshold = 2,
        MaxStockThreshold = 20
    },

    new Item
    {
        Id = 15,
        Caption = "Gold Dastband",
        Status = true,
        Discount = 2M,
        EligibleChangePriceRang = 8M,
        Weight = 4.55M,
        Type = Item.TypeEnum.Men,
        CategoryId = 19,      // Dastband
        MaterialId = 1,
        MetalId = 1,          // Yellow-Gold
        OccasionId = 7,       // Formal
        StyleId = 2,          // Modern
        Description = "4.5g yellow-gold dastband with filigree details.",
        CostPerGram = 9M,
        ShopId = 1,
        ManufactureId = 2,    // DUBAI-21KT
        Size = 7,             // ring size
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item15.jpg",
        AvailableStock = 4,
        RestockThreshold = 1,
        MaxStockThreshold = 10
    },

    new Item
    {
        Id = 16,
        Caption = "Gold Link Ring",
        Status = true,
        Discount = 0.00M,
        EligibleChangePriceRang = 5M,
        Weight = 3.95M,
        Type = Item.TypeEnum.Men,
        CategoryId = 9,       // Ring
        MaterialId = 1,
        MetalId = 2,          // Rose-Gold
        OccasionId = 10,      // Everyday
        StyleId = 5,          // Classic
        Description = "3g rose-gold link-band ring, built for daily wear.",
        CostPerGram = 6M,
        ShopId = 1,
        ManufactureId = 6,    // TURKEY-21KT
        Size = 10,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item16.jpg",
        AvailableStock = 20,
        RestockThreshold = 5,
        MaxStockThreshold = 30
    },
    new Item
    {
        Id = 17,
        Caption = "Gold Chandelier Earrings",
        Status = true,
        Discount = 1M,
        EligibleChangePriceRang = 1M,
        Weight = 6.4M,
        Type = Item.TypeEnum.Women,
        CategoryId = 17,      // Earring
        MaterialId = 1,
        MetalId = 1,          // Yellow-Gold
        OccasionId = 8,       // Party
        StyleId = 6,          // Trendy
        Description = "6g yellow-gold chandelier earrings with crystal drops.",
        CostPerGram = 7M,
        ShopId = 1,
        ManufactureId = 4,    // DUBAI-24KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item17.jpg",
        AvailableStock = 5,
        RestockThreshold = 2,
        MaxStockThreshold = 10
    },


    new Item
    {
        Id = 18,
        Caption = "Gold Ashqband ",
        Status = true,
        Discount = 2M,
        EligibleChangePriceRang = 3M,
        Weight = 22.23M,
        Type = Item.TypeEnum.Women,
        CategoryId = 18,      // Ashqband
        MaterialId = 1,
        MetalId = 1,          // Yellow-Gold
        OccasionId = 3,       // Anniversary
        StyleId = 5,          // Classic
        Description = "22g yellow-gold ashqband with heart motif.",
        CostPerGram = 11M,
        ShopId = 1,
        ManufactureId = 8,    // TURKEY-24KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item18.jpg",
        AvailableStock = 3,
        RestockThreshold = 1,
        MaxStockThreshold = 10
    },
    new Item
    {
        Id = 19,
        Caption = "Gold Multi-Layer Neck-Piece",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 7M,
        Weight = 45.6M,
        Type = Item.TypeEnum.Women,
        CategoryId = 11,      // Neck-piece
        MaterialId = 1,
        MetalId = 3,          // White-Gold
        OccasionId = 8,       // Party
        StyleId = 4,          // Contemporary
        Description = "45g white-gold multi-layer neck-piece with pearl accents.",
        CostPerGram = 9M,
        ShopId = 1,
        ManufactureId = 10,   // IRAQ-21KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item19.jpg",
        AvailableStock = 2,
        RestockThreshold = 1,
        MaxStockThreshold = 5
    },

    new Item
    {
        Id = 20,
        Caption = "Gold 12-Link Bracelet",
        Status = true,
        Discount = 0M,
        EligibleChangePriceRang = 0M,
        Weight = 21.25M,
        Type = Item.TypeEnum.Men,
        CategoryId = 8,       // Bracelet(12)
        MaterialId = 1,
        MetalId = 2,          // Rose-Gold
        OccasionId = 2,       // Engagement
        StyleId = 1,          // Traditional
        Description = "28g rose-gold bracelet featuring twelve interlocking links.",
        CostPerGram = 6.0M,
        ShopId = 1,
        ManufactureId = 5,    // TURKEY-18KT
        Size = 0,
        MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item20.jpg",
        AvailableStock = 9,
        RestockThreshold = 3,
        MaxStockThreshold = 20
    },

new Item
{
    Id = 21,
    Caption = "Delicate Gold Double-Bracelet Set",
    Status = true,
    Discount = 5M,
    EligibleChangePriceRang = 9M,
    Weight = 12.75M,
    Type = Item.TypeEnum.Women,
    CategoryId = 6,       // Bracelet(2)
    MaterialId = 1,       // Gold
    MetalId = 1,          // Yellow-Gold
    OccasionId = 11,      // Special Occasion
    StyleId = 3,          // Antique
    Description = "12g yellow-gold double-bracelet set with antique scrollwork.",
    CostPerGram = 15M,
    ShopId = 1,
    ManufactureId = 5,    // TURKEY-18KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item21.jpg",
    AvailableStock = 9,
    RestockThreshold = 3,
    MaxStockThreshold = 20
},

new Item
{
    Id = 22,
    Caption = "Chunky Gold Men’s Bangle",
    Status = true,
    Discount = 0.00M,
    EligibleChangePriceRang = 3M,
    Weight = 40.63M,
    Type = Item.TypeEnum.Men,
    CategoryId = 7,       // Bracelet(6)
    MaterialId = 1,
    MetalId = 2,          // Rose-Gold
    OccasionId = 5,       // Festival
    StyleId = 1,          // Traditional
    Description = "40g rose-gold bangle with bold traditional pattern.",
    CostPerGram = 8M,
    ShopId = 1,
    ManufactureId = 8,    // TURKEY-24KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item22.jpg",
    AvailableStock = 4,
    RestockThreshold = 1,
    MaxStockThreshold = 10
},

new Item
{
    Id = 23,
    Caption = "Gold 12-Link Cuff",
    Status = true,
    Discount = 0M,
    EligibleChangePriceRang = 2M,
    Weight = 48.0M,
    Type = Item.TypeEnum.Women,
    CategoryId = 8,       // Bracelet(12)
    MaterialId = 1,
    MetalId = 3,          // White-Gold
    OccasionId = 8,       // Party
    StyleId = 4,          // Contemporary
    Description = "48g white-gold cuff with twelve interlocking links.",
    CostPerGram = 8.5M,
    ShopId = 1,
    ManufactureId = 4,    // DUBAI-24KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item23.jpg",
    AvailableStock = 6,
    RestockThreshold = 2,
    MaxStockThreshold = 15
},


new Item
{
    Id = 24,
    Caption = "Slim Gold Ring",
    Status = true,
    Discount = 0M,
    EligibleChangePriceRang = 0M,
    Weight = 3.5M,
    Type = Item.TypeEnum.Women,
    CategoryId = 9,       // Ring
    MaterialId = 1,
    MetalId = 1,          // Yellow-Gold
    OccasionId = 6,       // Casual
    StyleId = 7,          // Minimalist
    Description = "3g yellow-gold slim band, perfect for everyday wear.",
    CostPerGram = 6M,
    ShopId = 1,
    ManufactureId = 1,    // DUBAI-18KT
    Size = 6,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item24.jpg",
    AvailableStock = 12,
    RestockThreshold = 3,
    MaxStockThreshold = 25
},

new Item
{
    Id = 25,
    Caption = "Bold Gold Statement Ring",
    Status = true,
    Discount = 0.00M,
    EligibleChangePriceRang = 1M,
    Weight = 5.66M,
    Type = Item.TypeEnum.Men,
    CategoryId = 9,       // Ring
    MaterialId = 1,
    MetalId = 2,          // Rose-Gold
    OccasionId = 10,      // Everyday
    StyleId = 6,          // Trendy
    Description = "5g rose-gold ring with geometric contemporary pattern.",
    CostPerGram = 8M,
    ShopId = 1,
    ManufactureId = 2,    // DUBAI-21KT
    Size = 11,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item25.jpg",
    AvailableStock = 8,
    RestockThreshold = 2,
    MaxStockThreshold = 20
},


new Item
{
    Id = 26,
    Caption = "Classic Gold Wedding Band",
    Status = true,
    Discount = 4M,
    EligibleChangePriceRang = 5M,
    Weight = 6.77M,
    Type = Item.TypeEnum.Women,
    CategoryId = 10,      // Wedding-Ring
    MaterialId = 1,
    MetalId = 1,          // Yellow-Gold
    OccasionId = 1,       // Wedding
    StyleId = 5,          // Classic
    Description = "6g yellow-gold wedding band in timeless classic finish.",
    CostPerGram = 17M,
    ShopId = 1,
    ManufactureId = 9,    // IRAQ-18KT
    Size = 7,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item26.jpg",
    AvailableStock = 14,
    RestockThreshold = 5,
    MaxStockThreshold = 30
},


new Item
{
    Id = 27,
    Caption = "Elegant Gold Drop Earrings",
    Status = true,
    Discount = 0M,
    EligibleChangePriceRang = 1M,
    Weight = 9.5M,
    Type = Item.TypeEnum.Women,
    CategoryId = 17,      // Earring
    MaterialId = 1,
    MetalId = 3,          // White-Gold
    OccasionId = 2,       // Engagement
    StyleId = 8,          // Bohemian
    Description = "3.5g white-gold drop earrings with hand-picked gemstone.",
    CostPerGram = 7M,
    ShopId = 1,
    ManufactureId = 6,    // TURKEY-21KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item27.jpg",
    AvailableStock = 10,
    RestockThreshold = 3,
    MaxStockThreshold = 20
},


new Item
{
    Id = 28,
    Caption = "Gold Pearl Accent Necklace",
    Status = true,
    Discount = 0M,
    EligibleChangePriceRang = 1M,
    Weight = 22.2M,
    Type = Item.TypeEnum.Women,
    CategoryId = 11,      // Neck-piece
    MaterialId = 1,
    MetalId = 2,          // Rose-Gold
    OccasionId = 4,       // Birthday
    StyleId = 2,          // Modern
    Description = "22g rose-gold necklace with freshwater pearl clusters.",
    CostPerGram = 7M,
    ShopId = 1,
    ManufactureId = 8,    // TURKEY-24KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item28.jpg",
    AvailableStock = 5,
    RestockThreshold = 1,
    MaxStockThreshold = 10
},


new Item
{
    Id = 29,
    Caption = "Gold Baraq",
    Status = true,
    Discount = 0M,
    EligibleChangePriceRang = 1M,
    Weight = 1.99M,
    Type = Item.TypeEnum.Kids,
    CategoryId = 12,      // Baraq
    MaterialId = 1,
    MetalId = 1,          // Yellow-Gold
    OccasionId = 5,       // Festival
    StyleId = 6,          // Trendy
    Description = "1.8g yellow-gold in baraq style, great for kids.",
    CostPerGram = 11M,
    ShopId = 1,
    ManufactureId = 1,    // DUBAI-18KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item29.jpg",
    AvailableStock = 25,
    RestockThreshold = 5,
    MaxStockThreshold = 50
},
new Item
{
    Id = 30,
    Caption = "Open-Cuff Gold Dastband",
    Status = true,
    Discount = 1M,
    EligibleChangePriceRang = 2M,
    Weight = 20.40M,
    Type = Item.TypeEnum.Men,
    CategoryId = 19,      // Dastband
    MaterialId = 1,
    MetalId = 3,          // White-Gold
    OccasionId = 3,       // Anniversary
    StyleId = 10,         // Art Deco
    Description = "20g white-gold open cuff with art-deco motifs.",
    CostPerGram = 9.0M,
    ShopId = 1,
    ManufactureId = 8,    // TURKEY-24KT
    Size = 0,
    MainPhoto = "https://goldbazarblob.blob.core.windows.net/development/items/item30.jpg",
    AvailableStock = 3,
    RestockThreshold = 1,
    MaxStockThreshold = 10
        }};
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

        if (!context.Governorates.Any())
        {

            await context.Governorates.AddRangeAsync(contextData.Governorates);
            logger.LogInformation("Seeded Governorates with {NumItems} Governorates", context.Governorates.Count());
            await context.SaveChangesAsync();
        }
        if (!context.Cities.Any())
        {

            await context.Cities.AddRangeAsync(contextData.Cities);
            logger.LogInformation("Seeded Cities with {NumItems} Cities", context.Cities.Count());
            await context.SaveChangesAsync();
        }
        if (!context.Shops.Any())
        {

            await context.Shops.AddRangeAsync(contextData.Shops);
            logger.LogInformation("Seeded shops with {NumItems} items", context.Shops.Count());
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
        if (!context.Manufactures.Any())
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
        if (!context.Occasions.Any())
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

        if (!context.Items.Any())
        {

            await context.Items.AddRangeAsync(contextData.Items);
            logger.LogInformation("Seeded items with {NumItems} items", context.Items.Count());
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

        if (!context.Governorates.Any())
        {

            await context.Governorates.AddRangeAsync(contextData.Governorates);
            logger.LogInformation("Seeded Governorates with {NumItems} Governorates", context.Governorates.Count());
            await context.SaveChangesAsync();
        }
        if (!context.Cities.Any())
        {

            await context.Cities.AddRangeAsync(contextData.Cities);
            logger.LogInformation("Seeded Cities with {NumItems} Cities", context.Cities.Count());
            await context.SaveChangesAsync();
        }
        if (!context.Shops.Any())
        {

            await context.Shops.AddRangeAsync(contextData.Shops);
            logger.LogInformation("Seeded shops with {NumItems} items", context.Shops.Count());
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

        if (!context.Manufactures.Any())
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

        if (!context.Occasions.Any())
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

        if (!context.Items.Any())
        {

            await context.Items.AddRangeAsync(contextData.Items);
            logger.LogInformation("Seeded items with {NumItems} items", context.Items.Count());
            await context.SaveChangesAsync();
        }
    }
}

