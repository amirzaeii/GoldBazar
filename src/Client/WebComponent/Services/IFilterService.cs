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

        //Task<IEnumerable<CatalogInfo>> GetAllMaterials();
        //Task<IEnumerable<CatalogInfo>> GetAllMetals();
        //Task<IEnumerable<CatalogInfo>> GetAllOccasions();
        //Task<IEnumerable<CatalogInfo>> GetAllStyles();
        Task<IEnumerable<CatalogInfo>> GetCatalogDataAsync(string category);
    }
}
