using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
