using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
