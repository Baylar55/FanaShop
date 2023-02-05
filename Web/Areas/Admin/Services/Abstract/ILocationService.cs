using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ILocationService
    {
        Task<LocationIndexVM> GetAsync();
        Task<LocationUpdateVM> GetUpdateModelAsync();
        Task<bool> CreateAsync(LocationCreateVM model);
        Task<bool> UpdateAsync(LocationUpdateVM model);
        Task<bool> DeleteAsync();
    }
}
