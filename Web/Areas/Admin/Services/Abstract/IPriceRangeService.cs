using Web.Areas.Admin.ViewModels.PriceRange;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IPriceRangeService
    {
        Task<PriceRangeIndexVM> GetAllAsync();
        Task<PriceRangeUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(PriceRangeCreateVM model);
        Task<bool> UpdateAsync(PriceRangeUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
