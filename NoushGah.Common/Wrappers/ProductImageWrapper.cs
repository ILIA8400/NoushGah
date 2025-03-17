using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers
{
    public class ProductImageWrapper
    {
        public string Path { get; set; }
        public int ProductId { get; set; }

        #region Navigations
        public ProductWrapper Product { get; set; }
        #endregion
    }
}
