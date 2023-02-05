using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService LocationService)
        {
            _locationService = LocationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _locationService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new LocationCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync()
        {
            var model = await _locationService.GetUpdateModelAsync();
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LocationCreateVM model)
        {
            var isSucceeded = await _locationService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(LocationUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _locationService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync()
        {
            var isSucceeded = await _locationService.DeleteAsync();
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
