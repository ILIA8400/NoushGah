using NoushGah.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Common.Wrappers
{
    public class BasketWrapper
    {
        public int Id { get; set; }
        public BasketStatusEnum BasketStatus { get; set; }
        public string UserId { get; set; }
    }
}
