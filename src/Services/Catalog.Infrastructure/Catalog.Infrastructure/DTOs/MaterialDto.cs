
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
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
}
