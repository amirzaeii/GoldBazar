using Catalog.Infrastructure.Models;

using GoldBazar.Shared.DTOs;

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
           .WithTags("Style");

        api.MapGet("/styles/{id}", GetStyleById)
           .WithName("GetStyleById")
           .WithSummary("Get style by ID")
           .WithTags("Style");

        api.MapPost("/styles", AddStyle)
           .WithName("AddStyle")
           .WithSummary("Add a new style")
           .WithTags("Style");

        api.MapPut("/styles/{id}", UpdateStyle)
           .WithName("UpdateStyle")
           .WithSummary("Update style details")
           .WithTags("Style");

        api.MapDelete("/styles/{id}", DeleteStyle)
           .WithName("DeleteStyle")
           .WithSummary("Delete a style")
           .WithTags("Style");

        //api.MapGet("/manufactures", GetAllManufacture)
        //    .WithName("All Manufacture List")
        //    .WithSummary("List of manufactures")
        //    .WithDescription("List of manufactures.")
        //    .WithTags("Manufacture");

        return app;
    }

    //Material
    public static async Task<Results<Ok<MaterialDTO[]>, NotFound>> GetAllMaterials(
     [AsParameters] CatalogServices services)
    {
        var materials = await services.Context.Materials.ToArrayAsync();

        if (!materials.Any())
        {
            return TypedResults.NotFound();
        }

        var materialDtos = materials.Select(m => new MaterialDTO(m.Id, m.Name)).ToArray();
        return TypedResults.Ok(materialDtos);
    }

    public static async Task<Results<Ok<MaterialDTO>, NotFound>> GetMaterialById(
        int id, 
        [AsParameters] CatalogServices services)
    {
        var material = await services.Context.Materials.FindAsync(id);

        return material is not null
            ? TypedResults.Ok(new MaterialDTO(material.Id, material.Name))
            : TypedResults.NotFound();
    }
    public static async Task<Results<Created<MaterialDTO>, BadRequest<string>>> AddMaterial(
    MaterialDTO materialDto,
    [AsParameters] CatalogServices services)
    {
        if (await services.Context.Materials.AnyAsync(m => m.Name == materialDto.Name))
        {
            return TypedResults.BadRequest($"A material with the name '{materialDto.Name}' already exists.");
        }

        var material = new Material
        {
            Name = materialDto.Name
         
        };

        services.Context.Materials.Add(material);
        await services.Context.SaveChangesAsync();

        // material.Id is now populated by EF after SaveChangesAsync
        var resultDto = new MaterialDTO(material.Id, material.Name);
        return TypedResults.Created($"/api/catalog/materials/{material.Id}", resultDto);
    }


    public static async Task<Results<Ok<MaterialDTO>, NotFound>> UpdateMaterial(
        int id, 
        MaterialDTO updatedMaterial,
        [AsParameters] CatalogServices services)
    {
        var material = await services.Context.Materials.FindAsync(id);

        if (material == null)
        {
            return TypedResults.NotFound();
        }

        material.Name = updatedMaterial.Name;
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new MaterialDTO(material.Id, material.Name));
    }

    public static async Task<Results<Ok<string>, NotFound>> DeleteMaterial(
        int id, 
        [AsParameters] CatalogServices services)
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
    // GET /api/catalog/metals
    public static async Task<Results<Ok<MetalDTO[]>, NotFound>> GetAllMetals(
        [AsParameters] CatalogServices services)
    {
        var metals = await services.Context.Metals
            .Include(m => m.Material)
            .ToArrayAsync();

        if (!metals.Any())
            return TypedResults.NotFound();

        var dtos = metals.Select(m =>
            new MetalDTO(m.Id, m.Name, m.MaterialId, m.Material.Name)).ToArray();

        return TypedResults.Ok(dtos);
    }

    // GET /api/catalog/metals/{id}
    public static async Task<Results<Ok<MetalDTO>, NotFound>> GetMetalById(
        int id,
        [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals
            .Include(m => m.Material)
            .FirstOrDefaultAsync(m => m.Id == id);

        return metal is not null
            ? TypedResults.Ok(new MetalDTO(metal.Id, metal.Name, metal.MaterialId, metal.Material.Name))
            : TypedResults.NotFound();
    }

    //POST /api/catalog/metals
    public static async Task<Results<Created<MetalDTO>, BadRequest<string>>> AddMetal(
        MetalDTO dto,
        [AsParameters] CatalogServices services)
    {
        var isDuplicate = await services.Context.Metals
            .AnyAsync(m => m.Name == dto.Name && m.MaterialId == dto.MaterialId);

        if (isDuplicate)
        {
            return TypedResults.BadRequest(
                $"A metal with the name '{dto.Name}' already exists for the selected material.");
        }

        var metal = new Metal
        {
            Name = dto.Name,
            MaterialId = dto.MaterialId
        };

        services.Context.Metals.Add(metal);
        await services.Context.SaveChangesAsync();

        var resultDto = new MetalDTO(metal.Id, metal.Name, metal.MaterialId, string.Empty);
        return TypedResults.Created($"/api/catalog/metals/{metal.Id}", resultDto);
    }


    //public static async Task<Results<Created<MetalDTO>, BadRequest<string>>> AddMetal(
    //    MetalDTO dto,
    //    [AsParameters] CatalogServices services)
    //{
    //    if (await services.Context.Metals.AnyAsync(m => m.Name == dto.Name))
    //    {
    //        return TypedResults.BadRequest($"A metal with the name '{dto.Name}' already exists.");
    //    }

    //    var metal = new Metal
    //    {
    //        Name = dto.Name,
    //        MaterialId = dto.MaterialId
    //    };

    //    services.Context.Metals.Add(metal);
    //    await services.Context.SaveChangesAsync();

    //    var resultDto = new MetalDTO(metal.Id, metal.Name, metal.MaterialId, string.Empty);
    //    return TypedResults.Created($"/api/catalog/metals/{metal.Id}", resultDto);
    //}
    // POST /api/catalog/metals
    //public static async Task<Results<Created<MetalDTO>, BadRequest<string>>> AddMetal(
    //    MetalDTO dto,
    //    [AsParameters] CatalogServices services)
    //{
    //    if (await services.Context.Metals.AnyAsync(m => m.Name == dto.Name))
    //    {
    //        return TypedResults.BadRequest($"A metal with the name '{dto.Name}' already exists.");
    //    }

    //    var material = await services.Context.Materials.FindAsync(dto.MaterialId);
    //    if (material is null)
    //    {
    //        return TypedResults.BadRequest($"Material with ID {dto.MaterialId} not found.");
    //    }

    //    var metal = new Metal
    //    {
    //        Name = dto.Name,
    //        MaterialId = dto.MaterialId
    //    };

    //    services.Context.Metals.Add(metal);
    //    await services.Context.SaveChangesAsync();

    //    var resultDto = new MetalDTO(metal.Id, metal.Name, metal.MaterialId, material.Name);
    //    return TypedResults.Created($"/api/catalog/metals/{metal.Id}", resultDto);
    //}


    // PUT /api/catalog/metals/{id}
    public static async Task<Results<Ok<MetalDTO>, NotFound, BadRequest<string>>> UpdateMetal(
        int id,
        MetalDTO dto,
        [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);
        if (metal is null)
            return TypedResults.NotFound();

        if (await services.Context.Metals.AnyAsync(m => m.Name == dto.Name && m.Id != id))
        {
            return TypedResults.BadRequest($"A metal with the name '{dto.Name}' already exists.");
        }

        metal.Name = dto.Name;
        metal.MaterialId = dto.MaterialId;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new MetalDTO(metal.Id, metal.Name, metal.MaterialId, string.Empty));
    }

    // DELETE /api/catalog/metals/{id}
    public static async Task<Results<Ok<string>, NotFound>> DeleteMetal(
        int id,
        [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);
        if (metal is null)
            return TypedResults.NotFound();

        services.Context.Metals.Remove(metal);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Metal with ID {id} deleted.");
    }



    //Ocassion
    // GET /api/catalog/occasions
    public static async Task<Results<Ok<OccasionDTO[]>, NotFound>> GetAllOccasions(
        [AsParameters] CatalogServices services)
    {
        var occasions = await services.Context.Occasions
            .AsNoTracking()
            .Select(o => new OccasionDTO(o.Id, o.Name))
            .ToArrayAsync();

        return occasions.Length > 0
            ? TypedResults.Ok(occasions)
            : TypedResults.NotFound();
    }

    // GET /api/catalog/occasions/{id}
    public static async Task<Results<Ok<OccasionDTO>, NotFound>> GetOccasionById(
        int id,
        [AsParameters] CatalogServices services)
    {
        var dto = await services.Context.Occasions
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OccasionDTO(o.Id, o.Name))
            .FirstOrDefaultAsync();

        return dto is not null
            ? TypedResults.Ok(dto)
            : TypedResults.NotFound();
    }

    // POST /api/catalog/occasions
    public static async Task<Results<Created<OccasionDTO>, BadRequest<string>>> AddOccasion(
       OccasionDTO dto,
       [AsParameters] CatalogServices services)
    {
        if (await services.Context.Occasions.AnyAsync(o => o.Name == dto.Name))
        {
            return TypedResults.BadRequest($"An occasion with the name '{dto.Name}' already exists.");
        }

        var occasion = new Occassion
        {
            Name = dto.Name
        };

        services.Context.Occasions.Add(occasion);
        await services.Context.SaveChangesAsync();

        var resultDto = new OccasionDTO(occasion.Id, occasion.Name);
        return TypedResults.Created($"/api/catalog/occasions/{occasion.Id}", resultDto);
    }



    // PUT /api/catalog/occasions/{id}
    public static async Task<Results<Ok<OccasionDTO>, NotFound, BadRequest<string>>> UpdateOccasion(
        int id,
        OccasionDTO dto,
        [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);
        if (occasion is null)
            return TypedResults.NotFound();

        if (await services.Context.Occasions.AnyAsync(o => o.Name == dto.Name && o.Id != id))
        {
            return TypedResults.BadRequest($"An occasion with the name '{dto.Name}' already exists.");
        }

        occasion.Name = dto.Name;
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new OccasionDTO(occasion.Id, occasion.Name));
    }

    // DELETE /api/catalog/occasions/{id}
    public static async Task<Results<Ok<string>, NotFound>> DeleteOccasion(
        int id,
        [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);
        if (occasion is null)
            return TypedResults.NotFound();

        services.Context.Occasions.Remove(occasion);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Occasion with ID {id} deleted.");
    }



    // GET /styles
    public static async Task<Results<Ok<StyleDTO[]>, NotFound>> GetAllStyles(
        [AsParameters] CatalogServices services)
    {
        var dtos = await services.Context.Styles
            .AsNoTracking()
            .Select(s => new StyleDTO(s.Id, s.Name))
            .ToArrayAsync();

        return dtos.Length > 0
            ? TypedResults.Ok(dtos)
            : TypedResults.NotFound();
    }

    // GET /styles/{id}
    public static async Task<Results<Ok<StyleDTO>, NotFound>> GetStyleById(
        int id,
        [AsParameters] CatalogServices services)
    {
        var dto = await services.Context.Styles
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new StyleDTO(s.Id, s.Name))
            .FirstOrDefaultAsync();

        return dto is not null
            ? TypedResults.Ok(dto)
            : TypedResults.NotFound();
    }

    // POST /styles
    public static async Task<Results<Created<StyleDTO>, BadRequest<string>>> AddStyle(
        StyleDTO styleDto,
        [AsParameters] CatalogServices services)
    {
        if (await services.Context.Styles.AnyAsync(s => s.Name == styleDto.Name))
        {
            return TypedResults.BadRequest(
                $"A style with the name '{styleDto.Name}' already exists.");
        }

        var maxId = await services.Context.Styles
            .Select(s => (int?)s.Id)
            .MaxAsync() ?? 0;

        var style = new Style
        {
            Id = maxId + 1,
            Name = styleDto.Name
        };

        services.Context.Styles.Add(style);
        await services.Context.SaveChangesAsync();

        var resultDto = new StyleDTO(style.Id, style.Name);
        return TypedResults.Created($"/api/catalog/styles/{style.Id}", resultDto);
    }

    // PUT /styles/{id}
    public static async Task<Results<Ok<StyleDTO>, NotFound, BadRequest<string>>> UpdateStyle(
        int id,
        StyleDTO updatedDto,
        [AsParameters] CatalogServices services)
    {
        var style = await services.Context.Styles.FindAsync(id);
        if (style is null)
            return TypedResults.NotFound();

        if (await services.Context.Styles
                .AnyAsync(s => s.Name == updatedDto.Name && s.Id != id))
        {
            return TypedResults.BadRequest(
                $"A style with the name '{updatedDto.Name}' already exists.");
        }

        style.Name = updatedDto.Name;
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new StyleDTO(style.Id, style.Name));
    }

    // DELETE /styles/{id}
    public static async Task<Results<Ok<string>, NotFound>> DeleteStyle(
        int id,
        [AsParameters] CatalogServices services)
    {
        var style = await services.Context.Styles.FindAsync(id);
        if (style is null)
            return TypedResults.NotFound();

        services.Context.Styles.Remove(style);
        await services.Context.SaveChangesAsync();

        return TypedResults.Ok($"Style with ID {id} deleted.");
    }

}




