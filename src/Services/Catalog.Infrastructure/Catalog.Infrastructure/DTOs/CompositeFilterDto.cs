using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
    public class CompositeFilterDto
    {
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }
        public string ProductType { get; set; }
        public string Material { get; set; }
        public string Metal { get; set; }
        public string Size { get; set; }
        public string Occasion { get; set; }
        public string Style { get; set; }
        public string Manufacturer { get; set; }


        // Constructor to initialize all properties with default values
        public CompositeFilterDto(
            double minWeight = 0,
            double maxWeight = 0,
            string productType = "",
            string material = "",
            string metal = "",
            string size = "",
            string occasion = "",
            string style = "",
            string manufacturer = "")
        {
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            ProductType = productType;
            Material = material;
            Metal = metal;
            Size = size;
            Occasion = occasion;
            Style = style;
            Manufacturer = manufacturer;
        }
    }
}
