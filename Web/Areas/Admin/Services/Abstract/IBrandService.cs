using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IBrandService
    {
        Task<BrandIndexVM> GetAllAsync();
        Task<BrandUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(BrandCreateVM model);
        Task<bool> UpdateAsync(BrandUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
