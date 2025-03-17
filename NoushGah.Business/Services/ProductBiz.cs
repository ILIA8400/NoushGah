using NoushGah.Business.Interfaces;
using NoushGah.Common.Wrappers;
using NoushGah.Common.Wrappers.HomeWrapper;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Services
{
    public class ProductBiz : IProductBiz
    {
        private readonly IProductRepository productRepository;

        public ProductBiz(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<ProductWrapper>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<List<ProductWrapper>> GetCoffeesOfTheDayAsync(int count = 5)
        {
            return await productRepository.GetCoffeesOfTheDayAsync(count);
        }

        // گرفتن اطلاعات صحفه اصلی
        public async Task<IndexWrapper> GetHomeIndexDataAsync()
        {
            var model = new IndexWrapper();
            model.PopularProducts = await GetPopularProductsAsync(10);
            model.Specialproducts = await GetSpecialProductsAsync(10);
            
            return model;
        }

        public async Task<List<ProductWrapper>> GetPopularProductsAsync(int count = 5)
        {
            return await productRepository.GetPopularProductsAsync(count);
        }

        public async Task<List<ProductWrapper>> GetSpecialProductsAsync(int count = 5)
        {
            return await productRepository.GetSpecialProductsAsync(count);
        }
    }
}
