using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Blog;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        #region Blog

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _blogService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _blogService.GetCreateModelAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _blogService.GetUpdateModelAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _blogService.DetailsAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlogCreateVM model)
        {
            var isSucceeded = await _blogService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(BlogUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceeded = await _blogService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _blogService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        #endregion

        #region BlogPhoto

        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _blogService.GetBlogPhotoUpdateAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, BlogPhotoUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceeded = await _blogService.UpdatePhotoAsync(id, model);
            if (!isSucceeded) return NotFound();
            return RedirectToAction("update", "Blog", new { id = model.BlogId });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, BlogPhotoDeleteVM model)
        {
            var isSucceeded = await _blogService.DeletePhotoAsync(id, model);
            if (!isSucceeded) return NotFound();
            return RedirectToAction("update", "Blog", new { id = model.BlogId });
        }

        #endregion
    }
}
