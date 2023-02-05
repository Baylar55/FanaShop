using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _sizeService.GetAllAsync();
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
            var model = await _sizeService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeCreateVM model)
        {
            var isSucceded = await _sizeService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, SizeUpdateVM model)
        {
            if (id != model.Id) return NotFound();
            bool isSucceded = await _sizeService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _sizeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
