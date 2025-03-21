﻿using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketWrapper> CreateBasket(BasketWrapper basket);
        Task UpdateBasket(BasketWrapper basket);
        Task DeleteBasket(BasketWrapper basket);
        Task<BasketWrapper> GetBasket(int basketId);
        Task<BasketWrapper> GetBasketByUserId(string userId);
        Task<List<BasketItemWrapper>> GetBasketItems(string userId);
        Task ClearBasket(string userId);
        Task AddItemToBasket(BasketItemWrapper item, string userId);
        Task AddItemsToBasket(List<BasketItemWrapper> basketItems, string userId);
        Task RemoveItemFromBasket(BasketItemWrapper item, string userId);
        Task ConfirmedBasket(int basketId);
        Task ResetBasket(int basketId);
    }
}
