using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class ContactComponentRepository : Repository<ContactComponent>, IContactComponentRepository
    {
        public ContactComponentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
