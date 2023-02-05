using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IPriceRangeRepository : IRepository<PriceRange>
    {
        Task<PriceRange> GetAsync();
    }
}
