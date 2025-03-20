using Microsoft.AspNetCore.Mvc;
using NoushGah.Business.Interfaces;

namespace NoushGah.Web.Controllers
{
    public class SignInController : Controller
    {
        private readonly ISignInBiz signInBiz;

        public SignInController(ISignInBiz signInBiz)
        {
            this.signInBiz = signInBiz;
        }

        // صحفه ورود کاربر
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string phoneNumber)
        {
            await signInBiz.Login(phoneNumber);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register(string phoneNumber)
        {
            await signInBiz.SignUp(phoneNumber);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInBiz.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
