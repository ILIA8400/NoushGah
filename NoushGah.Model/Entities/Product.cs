using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        #region Navigations

        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        #endregion
    }
}
