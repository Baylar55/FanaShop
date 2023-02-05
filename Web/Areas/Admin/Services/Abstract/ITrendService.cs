using Web.Areas.Admin.ViewModels.Trend;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ITrendService
    {
        Task<TrendIndexVM> GetAsync();
        Task<TrendUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(TrendCreateVM model);
        Task<bool> UpdateAsync(TrendUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
