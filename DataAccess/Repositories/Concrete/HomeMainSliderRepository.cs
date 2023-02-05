using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class HomeMainSliderRepository : Repository<HomeMainSlider>, IHomeMainSliderRepository
    {
        public HomeMainSliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
