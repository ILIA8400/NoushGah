using Microsoft.AspNetCore.Mvc;
using NoushGah.Business.Interfaces;

namespace NoushGah.Web.Controllers
{
    public class BaristaController : Controller
    {
        private readonly IOrderBiz orderBiz;

        public BaristaController(IOrderBiz orderBiz)
        {
            this.orderBiz = orderBiz;
        }

        // صحفه نمایش سفارشات
        public async Task<IActionResult> Index()
        {
            var model = await orderBiz.GetOrders();
            return View(model);
        }
    }
}
