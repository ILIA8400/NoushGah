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
            var basket = await noushGahDbContext.Baskets.Where(x => x.UserId == userId).SingleOrDefaultAsync();

            if (basket.BasketStatus == Model.Enums.BasketStatusEnum.Confirmed)
            {
                // برو به ادرس Barista/Index
                throw new Exception("شما یک سفارش در حال اماده سازی دارید لطفا تا اماده شدن ان صبر کنید سپس دوباره امتحان کنید");
            }

            item.BasketId = basket.Id;

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

        public async Task ConfirmedBasket(int basketId)
        {
            var basket = await noushGahDbContext.Baskets.SingleOrDefaultAsync(x => x.Id == basketId);

            if (basket == null) throw new NullReferenceException();

            basket.BasketStatus = Model.Enums.BasketStatusEnum.Confirmed;
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

        public async Task<List<BasketItemWrapper>> GetBasketItems(string userId)
        {
            var basket = await noushGahDbContext.Baskets.SingleOrDefaultAsync(x => x.UserId == userId);

            if (basket == null) throw new NullReferenceException();

            if (basket.BasketStatus == Model.Enums.BasketStatusEnum.Confirmed) return new List<BasketItemWrapper>();

            var items = await noushGahDbContext.BasketItems.Where(i => i.BasketId == basket.Id)
            .Include(x => x.Product).ThenInclude(x => x.ProductImages)
            .Select(x => new BasketItemWrapper
            {
                BasketId = basket.Id,
                Count = x.Count,
                ProductId = x.ProductId,
                Product = new ProductWrapper()
                {
                    Id = x.Product.Id,
                    CategoryId = x.Product.CategoryId,
                    Description = x.Product.Description,
                    Details = x.Product.Details,
                    IsSpecialOffer = x.Product.IsSpecialOffer,
                    Name = x.Product.Name,
                    Popularity = x.Product.Popularity,
                    Price = x.Product.Price,
                    ProductImages = x.Product.ProductImages.Select(c => new ProductImageWrapper
                    {
                        Path = c.Path,
                    }).ToList(),
                    ServingTime = x.Product.ServingTime
                }
            })
            .ToListAsync();

            return items;
        }

        public async Task RemoveItemFromBasket(BasketItemWrapper item, string userId)
        {
            var basket = await noushGahDbContext.Baskets.Where(x => x.UserId == userId).SingleOrDefaultAsync();

            if (basket.BasketStatus == Model.Enums.BasketStatusEnum.Confirmed)
            {
                // برو به ادرس Barista/Index
                throw new Exception("شما یک سفارش در حال اماده سازی دارید لطفا تا اماده شدن ان صبر کنید سپس دوباره امتحان کنید");
            }

            var existingItem = await noushGahDbContext.BasketItems
                .FirstOrDefaultAsync(b => b.BasketId == basket.Id && b.ProductId == item.ProductId);

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

        public async Task ResetBasket(int basketId)
        {
            var basket = await noushGahDbContext.Baskets
                .SingleOrDefaultAsync(x => x.Id == basketId);

            if (basket == null) throw new NullReferenceException();

            basket.BasketStatus = Model.Enums.BasketStatusEnum.Reset;

            var basketItems = await noushGahDbContext.BasketItems
                .Include(x => x.Product)
                .Where(x => x.BasketId == basketId)
                .ToListAsync();

            noushGahDbContext.RemoveRange(basketItems);

            decimal totalAmount = 0;
            var invoiceItems = new List<InvoiceItem>();

            foreach (var item in basketItems)
            {
                totalAmount += item.Count * item.Product.Price;

                invoiceItems.Add(new InvoiceItem
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = basket.UserId
                });
            }

            var invoice = new Invoice
            {
                CreatedDate = DateTime.Now,
                CreatedUserId = basket.UserId,
                UserId = basket.UserId,
                TotalAmount = totalAmount,
                IsDeleted = false,
                InvoiceItems = invoiceItems
            };

            await noushGahDbContext.Invoices.AddAsync(invoice);
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
