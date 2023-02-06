using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketWithProducts(string userId);
    }
}
