using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class TrendRepository : Repository<Trend>, ITrendRepository
    {
        public TrendRepository(AppDbContext context) : base(context)
        {
        }
    }
}
