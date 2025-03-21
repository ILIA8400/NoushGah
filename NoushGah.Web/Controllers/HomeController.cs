using Microsoft.AspNetCore.Mvc;
using NoushGah.Business.Interfaces;

namespace NoushGah.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductBiz productBiz;
        private readonly ISignInBiz signInBiz;

        public HomeController(IProductBiz productBiz, ISignInBiz signInBiz)
        {
            this.productBiz = productBiz;
            this.signInBiz = signInBiz;
        }

        // صحفه اصلی
        public async Task<IActionResult> Index()
        {
            // فعلا تا وقتی که لاگین درست بشه با اینا لاگین میکنیم
            //await signInBiz.SignUp("09104566765");
            await signInBiz.Login("09104566765");
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
