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
    public class BasketRepository : IBasketRepository
    {
        private readonly NoushGahDbContext noushGahDbContext;

        public BasketRepository(NoushGahDbContext noushGahDbContext)
        {
            this.noushGahDbContext = noushGahDbContext;
        }

        public async Task AddItemsToBasket(List<BasketItemWrapper> basketItems, string userId)
        {
            var basketId = await noushGahDbContext.Baskets.Where(x => x.UserId == userId).Select(v => v.Id).SingleOrDefaultAsync();

            foreach (var item in basketItems)
            {
                item.BasketId = basketId;
            }

            var basketIds = basketItems.Select(b => b.BasketId).Distinct().ToList();
            var productIds = basketItems.Select(b => b.ProductId).Distinct().ToList();

            var existingItems = await noushGahDbContext.BasketItems
                .Where(b => basketIds.Contains(b.BasketId) && productIds.Contains(b.ProductId) && !b.IsDeleted)
                .ToListAsync();

            var newItems = new List<BasketItem>();

            foreach (var item in basketItems)
            {
                var existingItem = existingItems.FirstOrDefault(b =>
                    b.BasketId == item.BasketId && b.ProductId == item.ProductId);

                if (existingItem != null)
                {
                    existingItem.Count += item.Count;
                    existingItem.UpdatedDate = DateTime.Now;
                    existingItem.UpdatedUserId = userId;
                }
                else
                {
                    var newItem = new BasketItem()
                    {
                        Count = item.Count,
                        CreatedUserId = userId,
                        CreatedDate = DateTime.Now,
                        BasketId = item.BasketId,
                        ProductId = item.ProductId,
                        IsDeleted = false,
                    };
                    newItems.Add(newItem);
                }
            }

            if (newItems.Any())
            {
                await noushGahDbContext.AddRangeAsync(newItems);
            }

            await noushGahDbContext.SaveChangesAsync();
        }


        public async Task AddItemToBasket(BasketItemWrapper item, string userId)
        {
            var basketId = await noushGahDbContext.Baskets.Where(x => x.UserId == userId).Select(v => v.Id).SingleOrDefaultAsync();
            item.BasketId = basketId;

            var existingItem = await noushGahDbContext.BasketItems
                .FirstOrDefaultAsync(b => b.BasketId == item.BasketId && b.ProductId == item.ProductId && !b.IsDeleted);

            if (existingItem != null)
            {
                existingItem.Count += item.Count;
                existingItem.UpdatedDate = DateTime.Now;
                existingItem.UpdatedUserId = userId;
            }
            else
            {
                var model = new BasketItem()
                {
                    ProductId = item.ProductId,
                    IsDeleted = false,
                    BasketId = item.BasketId,
                    Count = item.Count,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = userId,
                };
                await noushGahDbContext.BasketItems.AddAsync(model);
            }

            await noushGahDbContext.SaveChangesAsync();
        }


        public async Task ClearBasket(string userId)
        {
            var basket = await noushGahDbContext.Baskets.SingleOrDefaultAsync(b => b.UserId == userId);

            if (basket == null) throw new NullReferenceException();

            basket.BasketStatus = Model.Enums.BasketStatusEnum.Reset;
            var items = await noushGahDbContext.BasketItems.Where(x => x.BasketId == basket.Id).ToListAsync();
            noushGahDbContext.RemoveRange(items);

            await noushGahDbContext.SaveChangesAsync();
        }

        public async Task<BasketWrapper> CreateBasket(BasketWrapper basket)
        {
            var model = new Basket()
            {
                Id = basket.Id,
                UserId = basket.UserId,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                BasketStatus = Model.Enums.BasketStatusEnum.Reset,
                CreatedUserId = basket.UserId,
            };

            await noushGahDbContext.AddAsync(model);
            await noushGahDbContext.SaveChangesAsync();

            return ToWrapper(model);
        }

        public async Task DeleteBasket(BasketWrapper basket)
        {
            var model = await noushGahDbContext.Baskets.SingleOrDefaultAsync(b => b.Id == basket.Id);

            if (model == null) throw new NullReferenceException();

            model.IsDeleted = true;
            noushGahDbContext.Update(model);
            await noushGahDbContext.SaveChangesAsync();
        }

        public async Task<BasketWrapper> GetBasket(int basketId)
        {
            var model = await noushGahDbContext.Baskets.SingleOrDefaultAsync(b => b.Id == basketId && !b.IsDeleted);

            if (model == null) throw new NullReferenceException();

            return ToWrapper(model);
        }

        public async Task<BasketWrapper> GetBasketByUserId(string userId)
        {
            var model = await noushGahDbContext.Baskets.SingleOrDefaultAsync(b => b.UserId == userId && !b.IsDeleted);

            if (model == null) throw new NullReferenceException();

            return ToWrapper(model);
        }

        public async Task RemoveItemFromBasket(BasketItemWrapper item, string userId)
        {
            var existingItem = await noushGahDbContext.BasketItems
                .FirstOrDefaultAsync(b => b.BasketId == item.BasketId && b.ProductId == item.ProductId);

            if (existingItem == null)
            {
                throw new KeyNotFoundException("آیتم موردنظر در سبد خرید یافت نشد.");
            }

            if (existingItem.Count > 1)
            {
                existingItem.Count--;
                existingItem.UpdatedDate = DateTime.Now;
                existingItem.UpdatedUserId = userId;
            }
            else
            {
                noushGahDbContext.BasketItems.Remove(existingItem);
            }

            await noushGahDbContext.SaveChangesAsync();
        }


        public async Task UpdateBasket(BasketWrapper basket)
        {
            var model = await noushGahDbContext.Baskets.SingleOrDefaultAsync(b => b.Id == basket.Id  && !b.IsDeleted);

            if (model == null) throw new NullReferenceException();

            model.BasketStatus = basket.BasketStatus;
            model.UpdatedDate = DateTime.Now;
            model.UpdatedUserId = basket.UserId;
            await noushGahDbContext.SaveChangesAsync();
        }


        private BasketWrapper ToWrapper(Basket basket)
        {
            return new BasketWrapper()
            {
                Id = basket.Id,
                BasketStatus = basket.BasketStatus,
                UserId = basket.UserId,
            };
        }
    }
}
