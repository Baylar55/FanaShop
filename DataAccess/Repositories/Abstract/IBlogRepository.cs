using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> GetWithPhotosAsync(int id);
        Task<List<Blog>> GetAllWithCategoriesAsync();
        Task<List<Blog>> GetRelatedAsync(int id);
        Task<List<Blog>> PaginateBlogsAsync(int page, int take);
        Task<int> GetPageCountAsync(int take);
    }
}
