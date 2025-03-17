using NoushGah.Common.Wrappers;
using NoushGah.Common.Wrappers.HomeWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Interfaces
{
    public interface IProductBiz
    {
        Task<List<ProductWrapper>> GetPopularProductsAsync(int count = 5);
        Task<List<ProductWrapper>> GetSpecialProductsAsync(int count = 5);
        Task<List<ProductWrapper>> GetCoffeesOfTheDayAsync(int count = 5);
        Task<List<ProductWrapper>> GetAllAsync();

        // گرفتن اطلاعات صحفه اصلی
        Task<IndexWrapper> GetHomeIndexDataAsync();

    }
}
