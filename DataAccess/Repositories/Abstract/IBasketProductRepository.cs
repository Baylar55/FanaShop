using Core.Entities;
using System.Security.Claims;

namespace DataAccess.Repositories.Abstract
{
    public interface IBasketProductRepository : IRepository<BasketProduct>
    {
        Task<BasketProduct> GetBasketProducts(int modelId, int basketId);
        Task<bool> DeleteProductAsync(int productdId);
        Task<int> GetUserBasketProductsCount(ClaimsPrincipal user);
    }
}
