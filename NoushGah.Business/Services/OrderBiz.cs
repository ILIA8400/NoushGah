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
    public class OrderBiz : IOrderBiz
    {
        private readonly IOrderRepository orderRepository;

        public OrderBiz(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<List<OrderWrapper>> GetOrders()
        {
            return await orderRepository.GetOrders();
        }
    }
}
