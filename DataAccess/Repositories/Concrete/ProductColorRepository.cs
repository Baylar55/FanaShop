using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class ProductColorRepository : Repository<ProductColor>, IProductColorRepository
    {
        private readonly AppDbContext _context;

        public ProductColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ProductColor>> GetProductColorsAsync(int productId)
        {
            return await _context.ProductColor
                                             .Where(pc => pc.ProductId == productId)
                                             .ToListAsync();
        }
    }
}
