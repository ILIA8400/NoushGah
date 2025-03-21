using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers
{
    public class OrderWrapper
    {
        public int OrderId { get; set; } // شماره میز یا هرچیز یونیک دیگ

        public List<BasketItemWrapper> BasketItems { get; set; }
    }
}
