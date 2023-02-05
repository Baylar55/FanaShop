using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductSizeRepository : IRepository<ProductSize>
    {
        Task<List<ProductSize>> GetProductSizesAsync(int productId);
    }
}
