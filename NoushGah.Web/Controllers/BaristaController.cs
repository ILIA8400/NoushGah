using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class BaristaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
