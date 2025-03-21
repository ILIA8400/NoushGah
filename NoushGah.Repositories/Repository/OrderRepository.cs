using Microsoft.EntityFrameworkCore;
using NoushGah.Common.Wrappers;
using NoushGah.DataAccess;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NoushGahDbContext noushGahDbContext;

        public OrderRepository(NoushGahDbContext noushGahDbContext)
        {
            this.noushGahDbContext = noushGahDbContext;
        }

        public async Task<List<OrderWrapper>> GetOrders()
        {
            var basketIds = await noushGahDbContext.Baskets
                .Where(x=>x.BasketStatus == Model.Enums.BasketStatusEnum.Confirmed).Select(x=>x.Id).ToListAsync();

            var orders = await noushGahDbContext.BasketItems
                .Include(x => x.Product)
                .Where(x => basketIds.Contains(x.BasketId))
                .GroupBy(x => x.BasketId)
                .Select(x => new OrderWrapper
                {
                    OrderId = x.Key,
                    BasketItems = x.Select(item => new BasketItemWrapper
                    {
                        ProductId = item.ProductId,
                        BasketId = x.Key, // فعلا همون ایدی سبد خرید
                        Count = item.Count,
                        Product = new ProductWrapper
                        {
                            Id = item.ProductId,
                            Description = item.Product.Description,
                            Name = item.Product.Name,
                            Price = item.Product.Price,
                            ServingTime = item.Product.ServingTime,
                            Details = item.Product.Details,
                        }
                    }).ToList()
                }).ToListAsync();

            return orders;
        }
    }
}
