using Web.Areas.Admin.ViewModels.Feature;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFeatureService
    {
        Task<FeatureIndexVM> GetAsync();
        Task<FeatureUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(FeatureCreateVM model);
        Task<bool> UpdateAsync(FeatureUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
