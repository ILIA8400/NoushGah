using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoushGah.Business.Interfaces;
using NoushGah.Common.Wrappers;
using NoushGah.Model.Entities;
using System.Security.Claims;

namespace NoushGah.Web.Controllers
{
    //[Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketBiz basketBiz;

        public BasketController(IBasketBiz basketBiz)
        {
            this.basketBiz = basketBiz;
        }

        // نمایش سبد خرید
        public async Task<IActionResult> Index()
        {
            var model = await basketBiz.GetBasketItems(GetUserId());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ClearBasket()
        {
            try
            {
                await basketBiz.ClearBasket(GetUserId());

                return Json(new { success = true, message = "سبد خرید با موفقیت پاک شد." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(BasketItemWrapper basketItem)
        {
            try
            {
                await basketBiz.AddItemToBasket(basketItem, GetUserId());

                return Json(new { success = true, message = "ایتم با موفقیت اضافه شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddItemsToBasket(List<BasketItemWrapper> basketItems)
        {
            try
            {
                await basketBiz.AddItemsToBasket(basketItems, GetUserId());

                return Json(new { success = true, message = "ایتم ها با موفقیت اضافه شدند" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItemFromBasket(BasketItemWrapper basketItem)
        {
            try
            {
                await basketBiz.RemoveItemFromBasket(basketItem, GetUserId());

                return Json(new { success = true, message = "ایتم با موفقیت حذف شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
