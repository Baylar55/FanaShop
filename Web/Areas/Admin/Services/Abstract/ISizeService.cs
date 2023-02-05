using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ISizeService
    {
        Task<SizeIndexVM> GetAllAsync();
        Task<SizeUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(SizeCreateVM model);
        Task<bool> UpdateAsync(SizeUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
