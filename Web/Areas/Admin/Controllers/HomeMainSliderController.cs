using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {
        private readonly IHomeMainSliderService _homeMainSliderService;

        public HomeMainSliderController(IHomeMainSliderService homeMainSliderService)
        {
            _homeMainSliderService = homeMainSliderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _homeMainSliderService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new HomeMainSliderCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _homeMainSliderService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(HomeMainSliderCreateVM model)
        {
            var isSucceeded = await _homeMainSliderService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(HomeMainSliderUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _homeMainSliderService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _homeMainSliderService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

    }
}
