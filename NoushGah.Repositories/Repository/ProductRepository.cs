using Microsoft.EntityFrameworkCore;
using NoushGah.Common.Wrappers;
using NoushGah.DataAccess;
using NoushGah.Model.Entities;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly NoushGahDbContext context;

        public ProductRepository(NoushGahDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductWrapper>> GetAllAsync()
        {
            var products = await context.Products.ToListAsync();

            return products.Select(x => ToWrapper(x)).ToList();
        }

        public Task<List<ProductWrapper>> GetCoffeesOfTheDayAsync(int count = 5)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductWrapper>> GetPopularProductsAsync(int count = 5)
        {
            var products = await context.Products.Include(x => x.ProductImages)
                .OrderByDescending(p => p.Popularity)
                .Take(count)
                .ToListAsync();

            return products.Select(x => ToWrapper(x)).ToList();
        }

        public async Task<List<ProductWrapper>> GetSpecialProductsAsync(int count = 5)
        {
            var products = await context.Products.Include(x => x.ProductImages)
                .Where(x => x.IsSpecialOffer)
                .Take(count)
                .ToListAsync();

            return products.Select(x => ToWrapper(x)).ToList();
        }

        private ProductWrapper ToWrapper(Product product)
        {
            return new ProductWrapper()
            {
                Id = product.Id,
                Name = product.Name,
                IsSpecialOffer = product.IsSpecialOffer,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Popularity = product.Popularity,
                Details = product.Details,
                Price = product.Price,
                ServingTime = product.ServingTime,

                Category = product.Category != null
                    ? new CategoryWrapper
                    {
                        CategoryName = product.Category.CategoryName,
                        ParentCategoryId = product.Category.ParentCategoryId,
                        ParentCategory = product.Category.ParentCategory != null
                            ? new CategoryWrapper
                            {
                                CategoryName = product.Category.ParentCategory.CategoryName,
                                ParentCategoryId = product.Category.ParentCategory.ParentCategoryId
                            }
                            : null
                    }
                    : null,

                ProductImages = product.ProductImages != null
                    ? product.ProductImages.Select(img => new ProductImageWrapper
                    {
                        Path = img.Path,
                        ProductId = img.ProductId
                    }).ToList()
                    : new List<ProductImageWrapper>()
            };
        }

    }
}
