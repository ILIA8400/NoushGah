using NoushGah.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Entities
{
    public class Basket : BaseEntity
    {
        public BasketStatusEnum BasketStatus { get; set; }
        public int UserId { get; set; }
    }
}
