using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
    public record StyleDto
    {
        public int Id { get;}
        public string Name { get; }

        public StyleDto(Style style)
        {
            Id = style.Id;
            Name = style.Name;
        }
    }
}
