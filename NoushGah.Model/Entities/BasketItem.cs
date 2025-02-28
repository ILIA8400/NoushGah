using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int Count { get; set; }

        #region Navigations
        public Product Product { get; set; }
        public Basket Basket { get; set; } 
        #endregion
    }
}
