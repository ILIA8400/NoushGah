using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
