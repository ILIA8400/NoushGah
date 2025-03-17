using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers.HomeWrapper
{
    public class IndexWrapper
    {
        public List<ProductWrapper> Specialproducts { get; set; }
        public List<ProductWrapper> CoffeesOfTheDayProducts { get; set; }
        public List<ProductWrapper> PopularProducts { get; set; }
    }
}
