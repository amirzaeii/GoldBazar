
namespace Catalog.Infrastructure
{
    public class MetalDto
    {
        public int Id { get; }
        public string Name { get; }
        public string Manufacture { get; }
        public int KT { get; }

        public MetalDto(Metal metal)
        {
            Id = metal.Id;
            Name = metal.Name;
            Manufacture = metal.Manufacture;
            KT = metal.KT;
        }
    }
}
