using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Feature;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _featureService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new FeatureCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _featureService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FeatureCreateVM model)
        {
            var isSucceeded = await _featureService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(FeatureUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _featureService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _featureService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
