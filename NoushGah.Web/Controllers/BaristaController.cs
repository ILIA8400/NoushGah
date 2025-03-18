using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class BaristaController : Controller
    {

        // صحفه اصلی باریستا خودت باش
        public IActionResult Index()
        {
            return View();
        }
    }
}
