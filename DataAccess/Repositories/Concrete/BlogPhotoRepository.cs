using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class BlogPhotoRepository : Repository<BlogPhoto>, IBlogPhotoRepository
    {
        public BlogPhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
