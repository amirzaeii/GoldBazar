using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
    public record ProductTypeDto
    {
        public int Id { get;}
        public string Name { get; }

        public ProductTypeDto(ProductType productType)
        {
            Id = productType.Id;
            Name = productType.Name;
        }
    }
}
