using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Path { get; set; }
        public int ProductId { get; set; }

        #region Navigations
        public Product Product { get; set; } 
        #endregion
    }
}
