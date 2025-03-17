using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
