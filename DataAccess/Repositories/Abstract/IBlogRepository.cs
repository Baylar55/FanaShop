using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> GetWithPhotosAsync(int id);
        Task<List<Blog>> GetAllWithCategoriesAsync();
    }
}
