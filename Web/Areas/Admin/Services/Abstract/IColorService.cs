using Web.Areas.Admin.ViewModels.Color;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IColorService
    {
        Task<ColorIndexVM> GetAllAsync();
        Task<ColorUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(ColorCreateVM model);
        Task<bool> UpdateAsync(ColorUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
