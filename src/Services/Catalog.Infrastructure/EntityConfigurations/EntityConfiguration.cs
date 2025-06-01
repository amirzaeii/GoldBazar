
namespace Catalog.Infrastructure.EntityConfigurations;

public static class EntityConfiguration
{
    public static void CategoryConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("categoryseq")
            .StartsAt(20)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Category>(builder =>
        {
            builder.ToTable("Categories");
            builder.Property(b => b.Id)
                .UseHiLo("categoryseq");
        });
    }
    public static void CityConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("cityseq")
            .StartsAt(11)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<City>(builder =>
        {
            builder.ToTable("Cities");
            builder.Property(b => b.Id)
                .UseHiLo("cityseq");
        });
    }
    public static void GovernorateConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("governorseq")
            .StartsAt(20)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Governorate>(builder =>
        {
            builder.ToTable("Governorates");
            builder.Property(b => b.Id)
                .UseHiLo("governorseq");
        });
    }

    public static void ItemConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("itemseq")
            .StartsAt(20)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Item>(builder =>
        {
            builder.ToTable("Items");
            builder.Property(b => b.Id)
                .UseHiLo("itemseq");
        });
    }
    public static void ItemPhotoConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("itemphotoseq")
            .StartsAt(10)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<ItemPhoto>(builder =>
        {
            builder.ToTable("ItemPhotos");
            builder.Property(b => b.Id)
                .UseHiLo("itemphotoseq");
        });
    }
    public static void ShopConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("shopseq")
            .StartsAt(10)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Shop>(builder =>
        {
            builder.ToTable("Shops");
            builder.Property(b => b.Id)
                .UseHiLo("shopseq");
        });
    }
    public static void StyleConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("styleseq")
            .StartsAt(12)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Style>(builder =>
        {
            builder.ToTable("Styles");
            builder.Property(b => b.Id)
                .UseHiLo("styleseq");
        });
    }
    public static void OccassionConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("occasionseq")
            .StartsAt(12)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Occassion>(builder =>
        {
            builder.ToTable("Occassions");
            builder.Property(b => b.Id)
                .UseHiLo("occasionseq");
        });
    }
    public static void MetalConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("metalseq")
            .StartsAt(5)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Metal>(builder =>
        {
            builder.ToTable("Metals");
            builder.Property(b => b.Id)
                .UseHiLo("metalseq");
        });
    }
    public static void ManufactureConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("manufactureseq")
            .StartsAt(15)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Manufacture>(builder =>
        {
            builder.ToTable("Manufactures");
            builder.Property(b => b.Id)
                .UseHiLo("manufactureseq");
        });
    }
    public static void MaterialConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("materialseq")
            .StartsAt(10)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<Metal>(builder =>
        {
            builder.ToTable("Materials");
            builder.Property(b => b.Id)
                .UseHiLo("materialseq");
        });
    }
    public static void SliderConfigure(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence<int>("sliderseq")
            .StartsAt(10)       // optional, default is 1
            .IncrementsBy(1);  // optional, default is 1

        modelBuilder.Entity<PromotionSlider>(builder =>
        {
            builder.ToTable("PromotionSliders");
            builder.Property(b => b.Id)
                .UseHiLo("sliderseq");
        });
    }
}

