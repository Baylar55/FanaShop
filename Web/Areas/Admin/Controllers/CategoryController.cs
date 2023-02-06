using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Category;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
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
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var isSucceded = await _categoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateVM model)
        {
            if (id != model.Id) return NotFound();
            bool isSucceded = await _categoryService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
