using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;

        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }
        public async Task<IActionResult> Blog()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }
        public async Task<IActionResult> SingleBlog(int id)
        {
            var model = await _pageService.GetBlogByIdAsync(id);
            return View(model);
        }
        public async Task<IActionResult> FAQ()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }
        public async Task<IActionResult> About()
        {
            var model = await _pageService.GetAsync();
            return View(model);
        }
        public async Task<IActionResult> LoadQuestions(int id)
        {
            var questions = await _pageService.LoadQuestionsAsync(id);
            return PartialView("_QuestionPartial", questions);
            //return Json(questions);
        }

    }
}
