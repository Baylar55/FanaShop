using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FAQCategory;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FAQCategoryController : Controller
    {
        private readonly IFAQCategoryService _categoryService;

        public FAQCategoryController(IFAQCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _categoryService.GetAllAsync();
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
            var model = await _categoryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCategoryCreateVM model)
        {
            var isSucceeded = await _categoryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, FAQCategoryUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceeded = await _categoryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceeded = await _categoryService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

    }
}
