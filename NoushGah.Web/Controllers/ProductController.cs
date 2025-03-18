using Microsoft.AspNetCore.Mvc;

namespace NoushGah.Web.Controllers
{
    public class ProductController : Controller
    {

        // نمایش جزئیات محصول
        public async Task<IActionResult> Details(int productId)
        {
            return View();
        }
    }
}
