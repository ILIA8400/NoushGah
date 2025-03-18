using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class UserController : Controller
    {

        // پروفایل کاربر
        public IActionResult Profile(string userId)
        {
            return View();
        }
    }
}
