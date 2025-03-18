using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers
{
    public class BasketItemWrapper
    {
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int Count { get; set; }

        #region Navigations
        public ProductWrapper Product { get; set; }
        public BasketWrapper Basket { get; set; }
        #endregion
    }
}
