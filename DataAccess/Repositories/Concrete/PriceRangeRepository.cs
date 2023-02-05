using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class PriceRangeRepository : Repository<PriceRange>, IPriceRangeRepository
    {
        private readonly AppDbContext context;

        public PriceRangeRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<PriceRange> GetAsync()
        {
            return await context.PriceRange.FirstOrDefaultAsync();
        }
    }
}
