using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Apis;

public static class CatalogInfoApi
{
    public static IEndpointRouteBuilder MapCatalogInfoApiV1(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/catalog").HasApiVersion(1.0);

        // Material Endpoints
        api.MapGet("/materials", GetAllMaterials)
            .WithName("MaterialList")
            .WithSummary("List of materials")
            .WithDescription("Get a paginated list of materials.")
            .WithTags("Material");

        api.MapGet("/materials/{id}", GetMaterialById)
            .WithName("GetMaterialById")
            .WithSummary("Get material by ID")
            .WithDescription("Retrieve a material by its unique ID.")
            .WithTags("Material");

        api.MapPost("/materials", AddMaterial)
            .WithName("AddMaterial")
            .WithSummary("Add a new material")
            .WithDescription("Create a new material entry in the catalog.")
            .WithTags("Material");

        api.MapPut("/materials/{id}", UpdateMaterial)
            .WithName("UpdateMaterial")
            .WithSummary("Update material details")
            .WithDescription("Modify the details of an existing material.")
            .WithTags("Material");

        api.MapDelete("/materials/{id}", DeleteMaterial)
            .WithName("DeleteMaterial")
            .WithSummary("Delete a material")
            .WithDescription("Remove a material from the catalog by its ID.")
            .WithTags("Material");

        // Metal Endpoints
        api.MapGet("/metals", GetAllMetals)
            .WithName("MetalList")
            .WithSummary("List of metals")
            .WithDescription("Get a paginated list of metals.")
            .WithTags("Metal");

        api.MapGet("/metals/{id}", GetMetalById)
            .WithName("GetMetalById")
            .WithSummary("Get metal by ID")
            .WithDescription("Retrieve a metal by its unique ID.")
            .WithTags("Metal");

        api.MapPost("/metals", AddMetal)
            .WithName("AddMetal")
            .WithSummary("Add a new metal")
            .WithDescription("Create a new metal entry in the catalog.")
            .WithTags("Metal");

        api.MapPut("/metals/{id}", UpdateMetal)
            .WithName("UpdateMetal")
            .WithSummary("Update metal details")
            .WithDescription("Modify the details of an existing metal.")
            .WithTags("Metal");

        api.MapDelete("/metals/{id}", DeleteMetal)
            .WithName("DeleteMetal")
            .WithSummary("Delete a metal")
            .WithDescription("Remove a metal from the catalog by its ID.")
            .WithTags("Metal");

        // Occasion Endpoints
        api.MapGet("/occasions", GetAllOccasions)
            .WithName("OccasionList")
            .WithSummary("List of occasions")
            .WithDescription("Get a paginated list of occasions.")
            .WithTags("Occasion");

        api.MapGet("/occasions/{id}", GetOccasionById)
            .WithName("GetOccasionById")
            .WithSummary("Get occasion by ID")
            .WithDescription("Retrieve an occasion by its unique ID.")
            .WithTags("Occasion");

        api.MapPost("/occasions", AddOccasion)
            .WithName("AddOccasion")
            .WithSummary("Add a new occasion")
            .WithDescription("Create a new occasion entry in the catalog.")
            .WithTags("Occasion");

        api.MapPut("/occasions/{id}", UpdateOccasion)
            .WithName("UpdateOccasion")
            .WithSummary("Update occasion details")
            .WithDescription("Modify the details of an existing occasion.")
            .WithTags("Occasion");

        api.MapDelete("/occasions/{id}", DeleteOccasion)
            .WithName("DeleteOccasion")
            .WithSummary("Delete an occasion")
            .WithDescription("Remove an occasion from the catalog by its ID.")
            .WithTags("Occasion");

        // Style Endpoints
        api.MapGet("/styles", GetAllStyles)
            .WithName("StyleList")
            .WithSummary("List of styles")
            .WithDescription("Get a paginated list of styles.")
            .WithTags("Style");

        api.MapGet("/styles/{id}", GetStyleById)
            .WithName("GetStyleById")
            .WithSummary("Get style by ID")
            .WithDescription("Retrieve a style by its unique ID.")
            .WithTags("Style");

        api.MapPost("/styles", AddStyle)
            .WithName("AddStyle")
            .WithSummary("Add a new style")
            .WithDescription("Create a new style entry in the catalog.")
            .WithTags("Style");

        api.MapPut("/styles/{id}", UpdateStyle)
            .WithName("UpdateStyle")
            .WithSummary("Update style details")
            .WithDescription("Modify the details of an existing style.")
            .WithTags("Style");

        api.MapDelete("/styles/{id}", DeleteStyle)
            .WithName("DeleteStyle")
            .WithSummary("Delete a style")
            .WithDescription("Remove a style from the catalog by its ID.")
            .WithTags("Style");

 
        return app;
    }

    //Material
    public static async Task<Results<Ok<List<MaterialDto>>, NotFound>> GetAllMaterials(
     [AsParameters] CatalogServices services)
    {
        var materials = await services.Context.Materials.ToListAsync();

        if (!materials.Any())
        {
            return TypedResults.NotFound();
        }

        var materialDtos = materials.Select(m => new MaterialDto(m)).ToList();
        return TypedResults.Ok(materialDtos);
    }

    public static async Task<Results<Ok<MaterialDto>, NotFound>> GetMaterialById(
        int id, [AsParameters] CatalogServices services)
    {
        var material = await services.Context.Materials.FindAsync(id);

        return material is not null
            ? TypedResults.Ok(new MaterialDto(material))
            : TypedResults.NotFound();
    }

    public static async Task<Results<Created<MaterialDto>, BadRequest<string>>> AddMaterial(
        Material material, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Materials.AnyAsync(m => m.Name == material.Name))
        {
            return TypedResults.BadRequest($"A material with the name '{material.Name}' already exists.");
        }

        services.Context.Materials.Add(material);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/materials/{material.Id}", new MaterialDto(material));
    }

    public static async Task<Results<Ok<MaterialDto>, NotFound>> UpdateMaterial(
        int id, Material updatedMaterial, [AsParameters] CatalogServices services)
    {
        var material = await services.Context.Materials.FindAsync(id);

        if (material == null)
        {
            return TypedResults.NotFound();
        }

        material.Name = updatedMaterial.Name;
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new MaterialDto(material));
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteMaterial(
        int id, [AsParameters] CatalogServices services)
    {
        var material = await services.Context.Materials.FindAsync(id);

        if (material == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Materials.Remove(material);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Material with ID {id} deleted.");
    }

    //Metal
    public static async Task<Results<Ok<List<MetalDto>>, NotFound>> GetAllMetals(
     [AsParameters] CatalogServices services)
    {
        var metals = await services.Context.Metals.ToListAsync();

        if (!metals.Any())
        {
            return TypedResults.NotFound();
        }

        var metalDtos = metals.Select(m => new MetalDto(m)).ToList();
        return TypedResults.Ok(metalDtos);
    }

    public static async Task<Results<Ok<MetalDto>, NotFound>> GetMetalById(
        int id, [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);

        return metal is not null
            ? TypedResults.Ok(new MetalDto(metal))
            : TypedResults.NotFound();
    }

    public static async Task<Results<Created<MetalDto>, BadRequest<string>>> AddMetal(
        Metal metal, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Metals.AnyAsync(m => m.Name == metal.Name && m.Karat == metal.Karat))
        {
            return TypedResults.BadRequest($"A metal with the name '{metal.Name}' and karat {metal.Karat} already exists.");
        }

        services.Context.Metals.Add(metal);
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/metals/{metal.Id}", new MetalDto(metal));
    }

    public static async Task<Results<Ok<MetalDto>, NotFound>> UpdateMetal(
        int id, Metal updatedMetal, [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);

        if (metal == null)
        {
            return TypedResults.NotFound();
        }

        metal.Name = updatedMetal.Name;
        metal.Manufacture = updatedMetal.Manufacture;
        metal.Karat = updatedMetal.Karat;
        metal.Purity = updatedMetal.Purity;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new MetalDto(metal));
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteMetal(
        int id, [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);

        if (metal == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Metals.Remove(metal);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Metal with ID {id} deleted.");
    }


    //Ocassion
    // Get All Occasions
    public static async Task<Results<Ok<List<OccassionDTO>>, NotFound>> GetAllOccasions(
        [AsParameters] CatalogServices services)
    {
        var occasions = await services.Context.Occasions.ToListAsync();

        if (!occasions.Any())
        {
            return TypedResults.NotFound();
        }

        var occasionDtos = occasions.Select(o => new OccassionDTO(o)).ToList();
        return TypedResults.Ok(occasionDtos);
    }

    // Get Occasion By ID
    public static async Task<Results<Ok<OccassionDTO>, NotFound>> GetOccasionById(
        int id, [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);

        return occasion is not null
            ? TypedResults.Ok(new OccassionDTO(occasion))
            : TypedResults.NotFound();
    }

    // Add New Occasion
    public static async Task<Results<Created<OccassionDTO>, BadRequest<string>>> AddOccasion(
        Occassion occasion, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Occasions.AnyAsync(o => o.Name == occasion.Name))
        {
            return TypedResults.BadRequest($"An occasion with the name '{occasion.Name}' already exists.");
        }

        services.Context.Occasions.Add(occasion);
        await services.Context.SaveChangesAsync();

        var occasionDto = new OccassionDTO(occasion);
        return TypedResults.Created($"/occasions/{occasion.Id}", occasionDto);
    }

    // Update Existing Occasion
    public static async Task<Results<Ok<OccassionDTO>, NotFound, BadRequest<string>>> UpdateOccasion(
        int id, Occassion updatedOccasion, [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);

        if (occasion == null)
        {
            return TypedResults.NotFound();
        }

        // Check for uniqueness if name is changed
        if (occasion.Name != updatedOccasion.Name &&
            await services.Context.Occasions.AnyAsync(o => o.Name == updatedOccasion.Name && o.Id != id))
        {
            return TypedResults.BadRequest($"An occasion with the name '{updatedOccasion.Name}' already exists.");
        }

        occasion.Name = updatedOccasion.Name;

        await services.Context.SaveChangesAsync();
        return TypedResults.Ok(new OccassionDTO(occasion));
    }

    // Delete Occasion
    public static async Task<Results<Ok<string>, NotFound>> DeleteOccasion(
        int id, [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);

        if (occasion == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Occasions.Remove(occasion);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Occasion with ID {id} deleted.");
    }


    //styles
    // Get All Styles
    public static async Task<Results<Ok<List<StyleDto>>, NotFound>> GetAllStyles(
        [AsParameters] CatalogServices services)
    {
        var styles = await services.Context.Styles.ToListAsync();

        if (!styles.Any())
        {
            return TypedResults.NotFound();
        }

        var styleDtos = styles.Select(s => new StyleDto(s)).ToList();
        return TypedResults.Ok(styleDtos);
    }

    // Get Style By ID
    public static async Task<Results<Ok<StyleDto>, NotFound>> GetStyleById(
        int id, [AsParameters] CatalogServices services)
    {
        var style = await services.Context.Styles.FindAsync(id);

        return style is not null
            ? TypedResults.Ok(new StyleDto(style))
            : TypedResults.NotFound();
    }

    // Add New Style
    public static async Task<Results<Created<StyleDto>, BadRequest<string>>> AddStyle(
        Style style, [AsParameters] CatalogServices services)
    {
        if (await services.Context.Styles.AnyAsync(s => s.Name == style.Name))
        {
            return TypedResults.BadRequest($"A style with the name '{style.Name}' already exists.");
        }

        services.Context.Styles.Add(style);
        await services.Context.SaveChangesAsync();

        var styleDto = new StyleDto(style);
        return TypedResults.Created($"/styles/{style.Id}", styleDto);
    }

    // Update Existing Style
    public static async Task<Results<Ok<StyleDto>, NotFound, BadRequest<string>>> UpdateStyle(
        int id, Style updatedStyle, [AsParameters] CatalogServices services)
    {
        var style = await services.Context.Styles.FindAsync(id);

        if (style == null)
        {
            return TypedResults.NotFound();
        }

        // Ensure uniqueness during update
        if (style.Name != updatedStyle.Name &&
            await services.Context.Styles.AnyAsync(s => s.Name == updatedStyle.Name && s.Id != id))
        {
            return TypedResults.BadRequest($"A style with the name '{updatedStyle.Name}' already exists.");
        }

        style.Name = updatedStyle.Name;

        await services.Context.SaveChangesAsync();
        return TypedResults.Ok(new StyleDto(style));
    }

    // Delete Style
    public static async Task<Results<Ok<string>, NotFound>> DeleteStyle(
        int id, [AsParameters] CatalogServices services)
    {
        var style = await services.Context.Styles.FindAsync(id);

        if (style == null)
        {
            return TypedResults.NotFound();
        }

        services.Context.Styles.Remove(style);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Style with ID {id} deleted.");
    }

}




