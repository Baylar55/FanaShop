using Core.Entities;
using Web.ViewModels.Pages;

namespace Web.Services.Abstract
{
    public interface IPageService
    {
        Task<PagesIndexVM> GetAsync(PagesIndexVM model);
        Task<SingleBlogIndexVM> GetBlogByIdAsync(int id);
        Task<List<Question>> LoadQuestionsAsync(int id);
    }
}
