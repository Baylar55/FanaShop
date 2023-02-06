using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Blog> GetWithPhotosAsync(int id)
        {
            return await _context.Blog
                                      .Where(b => b.Id == id)
                                      .Include(b => b.BlogPhotos)
                                      .Include(b => b.Category)
                                      .FirstOrDefaultAsync(b => b.Id == id);
        }
        
        public async Task<List<Blog>> GetAllWithCategoriesAsync()
        {
            return await _context.Blog.Include(b => b.Category).ToListAsync();
        }

        public async Task<List<Blog>> GetRelatedAsync(int id)
        {
            return await _context.Blog
                                      .Include(b=>b.Category)
                                      .Where(b => b.CategoryId == id)
                                      .OrderByDescending(b => b.CreatedAt)
                                      .Take(2)
                                      .ToListAsync();
        }

        public async Task<int> GetPageCountAsync(int take)
        {
            var blogsCount = await _context.Blog.CountAsync();
            return (int)Math.Ceiling((decimal)blogsCount / take);
        }

        public async Task<List<Blog>> PaginateBlogsAsync(int page, int take)
        {
            return await _context.Blog
                 .Include(b => b.Category)
                 .OrderByDescending(b => b.Id)
                 .Skip((page - 1) * take).Take(take)
                 .ToListAsync();
        }
    }
}
