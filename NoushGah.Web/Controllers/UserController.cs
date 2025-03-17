using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
