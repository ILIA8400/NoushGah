using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class AdminPanelController : Controller
    {

        // پنل ادمین
        public IActionResult Index()
        {
            return View();
        }
    }
}
