using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.Model.Enums
{
    public enum BasketStatusEnum
    {
        Reset = 0,      // سبد خرید خالی شده
        InProgress = 1, // در حال خرید
        Confirmed = 2   // سفارش تأیید شده
    }
}
