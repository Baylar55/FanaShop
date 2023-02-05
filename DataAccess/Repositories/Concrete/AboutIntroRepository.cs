using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class AboutIntroRepository : Repository<AboutIntro>, IAboutIntroRepository
    {
        private readonly AppDbContext _context;

        public AboutIntroRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AboutIntro> GetAsync()
        {
            return await _context.AboutIntro.FirstOrDefaultAsync();
        }
    }
}
