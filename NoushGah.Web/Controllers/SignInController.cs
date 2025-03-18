using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class SignInController : Controller
    {

        // صحفه ورود کاربر
        public IActionResult Index()
        {
            return View();
        }
    }
}
