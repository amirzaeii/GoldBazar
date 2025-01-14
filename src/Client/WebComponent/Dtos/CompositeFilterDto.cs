using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebComponent.Dtos;

namespace WebComponent.Dtos
{
    public record CompositeFilterDto(
    decimal? MinWeight = 0,
    decimal? MaxWeight = 0,
    IEnumerable<int>? ProductType = null,
    IEnumerable<int>? Material = null,
    IEnumerable<int>? Metal = null,
    IEnumerable<int>? Occasion = null,
    IEnumerable<int>? Style = null
);

}
