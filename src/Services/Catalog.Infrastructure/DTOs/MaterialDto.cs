
namespace Catalog.Infrastructure;

public record MaterialDto
{
    public int Id { get; }
    public string Name { get; }
    public MaterialDto(Material material)
    {
        Id = material.Id;
        Name = material.Name;
    }
}
