using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ContactComponent;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class ContactComponentController : Controller
    {
        private readonly IContactComponentService _contactComponentService;

        public ContactComponentController(IContactComponentService contactComponentService)
        {
            _contactComponentService = contactComponentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _contactComponentService.GetAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var model = new ContactComponentCreateVM();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _contactComponentService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(ContactComponentCreateVM model)
        {
            var isSucceeded = await _contactComponentService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ContactComponentUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _contactComponentService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _contactComponentService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
