using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.StyleGallery;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class StyleGalleryController : Controller
    {
        private readonly IStyleGalleryService _styleGalleryService;

        public StyleGalleryController(IStyleGalleryService styleGalleryService)
        {
            _styleGalleryService = styleGalleryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _styleGalleryService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new StyleGalleryCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _styleGalleryService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(StyleGalleryCreateVM model)
        {
            var isSucceeded = await _styleGalleryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(StyleGalleryUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _styleGalleryService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _styleGalleryService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
