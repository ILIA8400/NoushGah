using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class BasketController : Controller
    {

        // سبد خرید
        public IActionResult Index()
        {
            return View();
        }
    }
}
