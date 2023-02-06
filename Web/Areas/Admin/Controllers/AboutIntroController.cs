using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutIntro;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutIntroController : Controller
    {
        private readonly IAboutIntroService _aboutIntroService;

        public AboutIntroController(IAboutIntroService AboutIntroService)
        {
            _aboutIntroService = AboutIntroService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _aboutIntroService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new AboutIntroCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync()
        {
            var model = await _aboutIntroService.GetUpdateModelAsync();
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AboutIntroCreateVM model)
        {
            var isSucceeded = await _aboutIntroService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(AboutIntroUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _aboutIntroService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync()
        {
            var isSucceeded = await _aboutIntroService.DeleteAsync();
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
