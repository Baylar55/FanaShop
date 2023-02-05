using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public IActionResult Cart()
        {
            return View();
        }

        public async Task<IActionResult> ProductList(ShopIndexVM model)
        {
            return View(await _shopService.GetAsync(model));
        }

        public async Task<IActionResult> SingleProduct(int id)
        {
            var model = await _shopService.GetProductByIdAsync(id);
            return View(model);
        }

        public async Task<IActionResult> LoadProducts(int id)
        {
            var products = await _shopService.LoadProductsAsync(id);
            return PartialView("_ProductPartial", products);
            //return Json(products);
        }
    }
}
