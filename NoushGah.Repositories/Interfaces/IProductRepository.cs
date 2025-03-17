using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductWrapper>> GetPopularProductsAsync(int count = 5);
        Task<List<ProductWrapper>> GetSpecialProductsAsync(int count = 5);
        Task<List<ProductWrapper>> GetCoffeesOfTheDayAsync(int count = 5);
        Task<List<ProductWrapper>> GetAllAsync();
    }
}
