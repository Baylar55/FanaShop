using Web.Areas.Admin.ViewModels.Feedback;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFeedbackService
    {
        Task<FeedbackIndexVM> GetAsync();
        Task<FeedbackUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(FeedbackCreateVM model);
        Task<bool> UpdateAsync(FeedbackUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
