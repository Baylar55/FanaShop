using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;

        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketWithProducts(string userId)
        {
            return await _context.Basket
                                        .Include(b => b.BasketProducts)
                                        .ThenInclude(bp => bp.Product)
                                        .FirstOrDefaultAsync(b => b.UserId == userId);
        }
    }
}
