using Web.Areas.Admin.ViewModels.StyleGallery;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IStyleGalleryService
    {
        Task<StyleGalleryIndexVM> GetAsync();
        Task<StyleGalleryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(StyleGalleryCreateVM model);
        Task<bool> UpdateAsync(StyleGalleryUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
