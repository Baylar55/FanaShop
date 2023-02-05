using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeMainSliderService
    {
        Task<HomeMainSliderIndexVM> GetAsync();
        Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(HomeMainSliderCreateVM model);
        Task<bool> UpdateAsync(HomeMainSliderUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
