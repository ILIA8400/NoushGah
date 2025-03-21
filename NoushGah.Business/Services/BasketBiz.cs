using NoushGah.Business.Interfaces;
using NoushGah.Common.Wrappers;
using NoushGah.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Services
{
    public class BasketBiz : IBasketBiz
    {
        private readonly IBasketRepository basketRepository;

        public BasketBiz(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task AddItemsToBasket(List<BasketItemWrapper> basketItems, string userId)
        {
            await basketRepository.AddItemsToBasket(basketItems, userId);
        }

        public async Task AddItemToBasket(BasketItemWrapper item, string userId)
        {
            await basketRepository.AddItemToBasket(item, userId);
        }

        public async Task ClearBasket(string userId)
        {
            await basketRepository.ClearBasket(userId);
        }

        public async Task ConfirmedBasket(int basketId)
        {
            await basketRepository.ConfirmedBasket(basketId);
        }

        public async Task<BasketWrapper> CreateBasket(BasketWrapper basket)
        {
            return await basketRepository.CreateBasket(basket);
        }

        public async Task DeleteBasket(BasketWrapper basket)
        {
            await basketRepository.DeleteBasket(basket);
        }

        public async Task<BasketWrapper> GetBasket(int basketId)
        {
            return await basketRepository.GetBasket(basketId);
        }

        public async Task<BasketWrapper> GetBasketByUserId(string userId)
        {
            return await basketRepository.GetBasketByUserId(userId);
        }

        public async Task<List<BasketItemWrapper>> GetBasketItems(string userId)
        {
            return await basketRepository.GetBasketItems(userId);
        }

        public async Task RemoveItemFromBasket(BasketItemWrapper item, string userId)
        {
            await basketRepository.RemoveItemFromBasket(item, userId);
        }

        public async Task UpdateBasket(BasketWrapper basket)
        {
            await basketRepository.UpdateBasket(basket);
        }
    }
}
