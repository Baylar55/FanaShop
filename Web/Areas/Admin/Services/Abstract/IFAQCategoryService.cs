using Web.Areas.Admin.ViewModels.FAQCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFAQCategoryService
    {
        Task<FAQCategoryIndexVM> GetAllAsync();
        Task<FAQCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(FAQCategoryCreateVM model);
        Task<bool> UpdateAsync(FAQCategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
