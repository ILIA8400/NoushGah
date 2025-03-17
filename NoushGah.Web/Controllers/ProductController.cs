using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
