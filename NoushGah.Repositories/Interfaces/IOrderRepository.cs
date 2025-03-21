using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<OrderWrapper>> GetOrders();

    }
}
