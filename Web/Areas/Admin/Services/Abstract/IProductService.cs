using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<List<SelectListItem>> GetAllBrandsAsync();
        Task<List<SelectListItem>> GetAllCategoriesAsync();
        Task<ProductCreateVM> GetBrandCreateModelAsync();
        Task<bool> CreateAsync(ProductCreateVM model);
        Task DeleteAsync(int id);
        Task<ProductIndexVM> GetAllAsync();
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ProductUpdateVM model);
        Task<bool> AddColorAsync(ProductAddColorVM model);
        Task<bool> AddSizeAsync(ProductAddSizeVM model);
        Task<ProductAddColorVM> GetColorAddModelAsync();
        Task<ProductAddSizeVM> GetSizeAddModelAsync();
        Task<List<SelectListItem>> GetColorsSelectAsync();
        Task<List<SelectListItem>> GetSizesSelectAsync();
        Task<ProductDetailsVM> GetProductDetailsAsync(int id);
        Task<bool> PhotoUpdateAsync(ProductPhotoUpdateVM model);
        Task<ProductPhotoUpdateVM> GetPhotoUpdateModelAsync(int id);
        Task DeletePhotoAsync(int id);
    }
}
