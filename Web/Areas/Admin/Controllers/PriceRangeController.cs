using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.PriceRange;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class PriceRangeController : Controller
    {
        private readonly IPriceRangeService _priceRangeService;

        public PriceRangeController(IPriceRangeService priceRangeService)
        {
            _priceRangeService = priceRangeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _priceRangeService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _priceRangeService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PriceRangeCreateVM model)
        {
            var isSucceded = await _priceRangeService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, PriceRangeUpdateVM model)
        {
            if (id != model.Id) return NotFound();
            bool isSucceded = await _priceRangeService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _priceRangeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
