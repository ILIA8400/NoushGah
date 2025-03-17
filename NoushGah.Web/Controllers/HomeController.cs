using Microsoft.AspNetCore.Mvc;
using NoushGah.Business.Interfaces;

namespace NoushGah.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductBiz productBiz;

        public HomeController(IProductBiz productBiz)
        {
            this.productBiz = productBiz;
        }

        // صحفه اصلی
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "خانه";
            var model = await productBiz.GetHomeIndexDataAsync();
            return View(model);
        }

        // درباره ما
        public Task<IActionResult> AboutUs()
        {
            return null;
        }

        // تماس با ما
        public Task<IActionResult> ContactUs()
        {
            return null;
        }
    }
}
