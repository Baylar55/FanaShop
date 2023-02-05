using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class ProductSizeRepository : Repository<ProductSize>, IProductSizeRepository
    {
        private readonly AppDbContext _context;

        public ProductSizeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ProductSize>> GetProductSizesAsync(int productId)
        {
            var sizes = await _context.ProductSize
                                                  .Where(pc => pc.ProductId == productId)
                                                  .ToListAsync();
            return sizes;
        }
    }
}
