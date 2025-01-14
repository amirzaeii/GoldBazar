using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebComponent.Dtos;

namespace WebComponent.Services
{
    public interface IFilterService
    {

        Task<IEnumerable<CatalogItem>> FilterCatalogItemsAsync(CompositeFilterDto filterDto);
        Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category);
    }
}
