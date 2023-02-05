using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductColorRepository : IRepository<ProductColor>
    {
        Task<List<ProductColor>> GetProductColorsAsync(int productId);
    }
}
