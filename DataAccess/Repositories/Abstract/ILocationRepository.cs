using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> GetAsync();
    }
}
