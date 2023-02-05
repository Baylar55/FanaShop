using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Characteristic;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CharacteristicController : Controller
    {
        private readonly ICharacteristicService _characteristicService;

        public CharacteristicController(ICharacteristicService characteristicService)
        {
            _characteristicService = characteristicService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _characteristicService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new CharacteristicCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _characteristicService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CharacteristicCreateVM model)
        {
            var isSucceeded = await _characteristicService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(CharacteristicUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _characteristicService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _characteristicService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
