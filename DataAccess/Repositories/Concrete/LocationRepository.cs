using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly AppDbContext context;

        public LocationRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Location> GetAsync()
        {
            return await context.Location.FirstOrDefaultAsync();
        }
    }
}
