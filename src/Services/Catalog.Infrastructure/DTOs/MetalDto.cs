
using GoldBazar.Shared.DTOs;

namespace Catalog.Infrastructure;

public class MetalDto
{
    public int Id { get; }
    public string Name { get; }
    public ManufactureEnum Manufacture { get; } // Enum type for Manufacture
    public int KT { get; }

    public MetalDto(Metal metal)
    {
        Id = metal.Id;
        Name = metal.Name;
        Manufacture = metal.Manufacture;
        KT = metal.Karat;
    }
}
