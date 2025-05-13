﻿using GoldBazar.Shared.DTOs;

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
        MaterialDTO material, 
        [AsParameters] CatalogServices services)
    {
        if (await services.Context.Materials.AnyAsync(m => m.Name == material.Name))
        {
            return TypedResults.BadRequest($"A material with the name '{material.Name}' already exists.");
        }

        services.Context.Materials.Add(new Material
        {
            Name = material.Name
        });
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/materials/{material.Id}", new MaterialDTO(material.Id, material.Name));
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
    public static async Task<Results<Ok<MetalDTO[]>, NotFound>> GetAllMetals(
     [AsParameters] CatalogServices services)
    {
        var metals = await services.Context.Metals.Include(m => m.Material).ToArrayAsync();

        if (!metals.Any())
        {
            return TypedResults.NotFound();
        }

        var metalDtos = metals.Select(m => new MetalDTO(m.Id, m.Name, m.MaterialId, m.Material.Name)).ToArray();
        return TypedResults.Ok(metalDtos);
    }

    public static async Task<Results<Ok<MetalDTO>, NotFound>> GetMetalById(
        int id, 
        [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.Include(m => m.Material).FirstOrDefaultAsync(m => m.Id == id);

        return metal is not null
            ? TypedResults.Ok(new MetalDTO(metal.Id, metal.Name, metal.MaterialId, metal.Material.Name))
            : TypedResults.NotFound();
    }

    public static async Task<Results<Created<MetalDTO>, BadRequest<string>>> AddMetal(
        MetalDTO metal, 
        [AsParameters] CatalogServices services)
    {
        if (await services.Context.Metals.AnyAsync(m => m.Name == metal.name))
        {
            return TypedResults.BadRequest($"A metal with the name '{metal.name}'");
        }

        services.Context.Metals.Add(new Metal
        {
            Name = metal.name,
            MaterialId = metal.materialId
        });
        await services.Context.SaveChangesAsync();

        return TypedResults.Created($"/metals/{metal.id}", new MetalDTO(metal.id, metal.name, metal.materialId, string.Empty));
    }

    public static async Task<Results<Ok<MetalDTO>, NotFound>> UpdateMetal(
        int id, 
        MetalDTO updatedMetal, 
        [AsParameters] CatalogServices services)
    {
        var metal = await services.Context.Metals.FindAsync(id);

        if (metal == null)
        {
            return TypedResults.NotFound();
        }

        metal.Name = updatedMetal.name;
        metal.MaterialId = updatedMetal.materialId;

        await services.Context.SaveChangesAsync();

        return TypedResults.Ok(new MetalDTO(metal.Id, metal.Name, metal.MaterialId, string.Empty));
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
    public static async Task<Results<Ok<OccasionDTO[]>, NotFound>> GetAllOccasions(
        [AsParameters] CatalogServices services)
    {
        var occasions = await services.Context.Occasions.ToArrayAsync();

        if (!occasions.Any())
        {
            return TypedResults.NotFound();
        }

        var occasionDtos = occasions.Select(o => new OccasionDTO(o.Id, o.Name)).ToArray();
        return TypedResults.Ok(occasionDtos);
    }

    // Get Occasion By ID
    public static async Task<Results<Ok<OccasionDTO>, NotFound>> GetOccasionById(
        int id, [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(id);

        return occasion is not null
            ? TypedResults.Ok(new OccasionDTO(occasion.Id, occasion.Name))
            : TypedResults.NotFound();
    }

    // Add New Occasion
    public static async Task<Results<Created<OccasionDTO>, BadRequest<string>>> AddOccasion(
        OccasionDTO occasion, 
        [AsParameters] CatalogServices services)
    {
        if (await services.Context.Occasions.AnyAsync(o => o.Name == occasion.Name))
        {
            return TypedResults.BadRequest($"An occasion with the name '{occasion.Name}' already exists.");
        }

        services.Context.Occasions.Add(new Occassion
        {
            Name = occasion.Name
        });
        await services.Context.SaveChangesAsync();

        var occasionDto = new OccasionDTO(occasion.Id, occasion.Name);
        return TypedResults.Created($"/occasions/{occasion.Id}", occasionDto);
    }

    // Update Existing Occasion
    public static async Task<Results<Ok<OccasionDTO>, NotFound, BadRequest<string>>> UpdateOccasion(
        OccasionDTO updatedOccasion, 
        [AsParameters] CatalogServices services)
    {
        var occasion = await services.Context.Occasions.FindAsync(updatedOccasion.Id);
        if (occasion == null)
        {
            return TypedResults.NotFound();
        }

        occasion.Name = updatedOccasion.Name;

        await services.Context.SaveChangesAsync();
        return TypedResults.Ok(new OccasionDTO(occasion.Id, occasion.Name));
    }

    // Delete Occasion
    public static async Task<Results<Ok<string>, NotFound>> DeleteOccasion(
        int id, 
        [AsParameters] CatalogServices services)
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


    ////styles
    //// GET /styles
    //public static async Task<Results<Ok<StyleDTO[]>, NotFound>> GetAllStyles(
    //    [AsParameters] CatalogServices services)
    //{
    //    var styles = await services.Context.Styles
    //                         .AsNoTracking()
    //                         .ToArrayAsync();

    //    if (styles.Length == 0)
    //        return TypedResults.NotFound();

    //    var dtos = styles
    //               .Select(s => new StyleDTO(s.Id, s.Name))
    //               .ToArray();

    //    return TypedResults.Ok(dtos);
    //}

    //// GET /styles/{id}
    //public static async Task<Results<Ok<StyleDTO>, NotFound>> GetStyleById(
    //    int id,
    //    [AsParameters] CatalogServices services)
    //{
    //    var style = await services.Context.Styles
    //                          .AsNoTracking()
    //                          .FirstOrDefaultAsync(s => s.Id == id);

    //    return style is not null
    //         ? TypedResults.Ok(new StyleDTO(style.Id, style.Name))
    //         : TypedResults.NotFound();
    //}

    //// POST /styles
    //public static async Task<Results<Created<StyleDTO>, BadRequest<string>>> AddStyle(
    // StyleDTO styleDto,
    // [AsParameters] CatalogServices services)
    //{
    //    // 1) Prevent duplicate names
    //    if (await services.Context.Styles
    //              .AnyAsync(s => s.Name == styleDto.Name))
    //    {
    //        return TypedResults.BadRequest(
    //            $"A style with the name '{styleDto.Name}' already exists.");
    //    }

    //    // 2) Manually generate a new ID
    //    var maxId = await services.Context.Styles
    //                         .Select(s => (int?)s.Id)
    //                         .MaxAsync()
    //               ?? 0;

    //    var style = new Style
    //    {
    //        Id = maxId + 1,
    //        Name = styleDto.Name
    //    };

    //    // 3) Add & save
    //    services.Context.Styles.Add(style);
    //    await services.Context.SaveChangesAsync();

    //    // 4) Return the created DTO
    //    var resultDto = new StyleDTO(style.Id, style.Name);
    //    return TypedResults.Created($"/api/catalog/styles/{style.Id}", resultDto);
    //}


    //// PUT /styles/{id}
    //public static async Task<Results<Ok<StyleDTO>, NotFound, BadRequest<string>>> UpdateStyle(
    //    int id,
    //    StyleDTO updatedDto,
    //    [AsParameters] CatalogServices services)
    //{
    //    var style = await services.Context.Styles.FindAsync(id);
    //    if (style is null)
    //        return TypedResults.NotFound();

    //    // Prevent renaming to an existing name
    //    var conflict = await services.Context.Styles
    //                      .AnyAsync(s => s.Name == updatedDto.Name && s.Id != id);
    //    if (conflict)
    //        return TypedResults.BadRequest(
    //            $"A style with the name '{updatedDto.Name}' already exists.");

    //    style.Name = updatedDto.Name;
    //    // EF Core will issue UPDATE
    //    await services.Context.SaveChangesAsync();

    //    return TypedResults.Ok(new StyleDTO(style.Id, style.Name));
    //}

    //// DELETE /styles/{id}
    //public static async Task<Results<Ok<string>, NotFound>> DeleteStyle(
    //    int id,
    //    [AsParameters] CatalogServices services)
    //{
    //    var style = await services.Context.Styles.FindAsync(id);
    //    if (style is null)
    //        return TypedResults.NotFound();

    //    services.Context.Styles.Remove(style);
    //    await services.Context.SaveChangesAsync();

    //    return TypedResults.Ok($"Style with ID {id} deleted.");
    //}

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




