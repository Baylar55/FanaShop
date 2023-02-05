using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.ViewModels.Wishlist;

namespace Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService WishlistService)
        {
            _wishlistService = WishlistService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _wishlistService.GetAllAsync();
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Add(WishlistAddVM model)
        {
            var isSucceded = await _wishlistService.WishlistAddAsync(model);
            if (isSucceded) return Ok();
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _wishlistService.RemoveAsync(id);
            if (isSucceded) return Ok();
            return NotFound();
        }
    }
}
