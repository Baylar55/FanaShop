using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Color;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _colorService.GetAllAsync();
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
            var model = await _colorService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorCreateVM model)
        {
            var isSucceded = await _colorService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ColorUpdateVM model)
        {
            if (id != model.Id) return NotFound();
            bool isSucceded = await _colorService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
