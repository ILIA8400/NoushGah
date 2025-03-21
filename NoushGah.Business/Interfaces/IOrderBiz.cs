using NoushGah.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Business.Interfaces
{
    public interface IOrderBiz
    {
        Task<List<OrderWrapper>> GetOrders();
    }
}
