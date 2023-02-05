using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionService
    {
        Task<QuestionIndexVM> GetAsync();
        Task<QuestionCreateVM> GetCreateModelAsync();
        Task<QuestionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(QuestionCreateVM model);
        Task<bool> UpdateAsync(QuestionUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
