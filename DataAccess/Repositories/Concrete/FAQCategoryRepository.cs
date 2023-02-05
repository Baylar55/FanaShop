using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class FAQCategoryRepository : Repository<FAQCategory>, IFAQCategoryRepository
    {
        public FAQCategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
