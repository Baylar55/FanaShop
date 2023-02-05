using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class ProductPhotoRepository : Repository<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
