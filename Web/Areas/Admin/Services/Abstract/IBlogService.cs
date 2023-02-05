using Web.Areas.Admin.ViewModels.Blog;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IBlogService
    {
        Task<BlogIndexVM> GetAllAsync();
        Task<BlogCreateVM> GetCreateModelAsync();
        Task<BlogUpdateVM> GetUpdateModelAsync(int id);
        Task<BlogDetailsVM> DetailsAsync(int id);
        Task<BlogPhotoUpdateVM> GetBlogPhotoUpdateAsync(int id);
        Task<bool> CreateAsync(BlogCreateVM model);
        Task<bool> UpdateAsync(BlogUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdatePhotoAsync(int id, BlogPhotoUpdateVM model);
        Task<bool> DeletePhotoAsync(int id, BlogPhotoDeleteVM model);
    }
}
