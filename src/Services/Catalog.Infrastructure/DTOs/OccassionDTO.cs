
namespace Catalog.Infrastructure;

public record OccassionDTO
{
    public int Id { get;  }
    public string Name { get; }

    public OccassionDTO(Occassion occassion)
    {
        Id = occassion.Id;
        Name = occassion.Name;
    }
}
