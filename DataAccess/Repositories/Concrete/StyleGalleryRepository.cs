using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class StyleGalleryRepository : Repository<StyleGallery>, IStyleGalleryRepository
    {
        public StyleGalleryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
