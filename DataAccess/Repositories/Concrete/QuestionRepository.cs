using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {

        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Question>> GetAllByCategoryAsync(int id)
        {
            return await _context.Question.Where(q => q.FAQCategoryId == id).ToListAsync();
        }

        public async Task<List<Question>> GetAllWithCategoriesAsync()
        {
            return await _context.Question
                            .OrderByDescending(q => q.Id)
                            .Include(q => q.FAQCategory)
                            .Take(6)
                            .ToListAsync();
        }
    }

}
