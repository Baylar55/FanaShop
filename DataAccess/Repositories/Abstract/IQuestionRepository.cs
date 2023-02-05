using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetAllByCategoryAsync(int id);

        Task<List<Question>> GetAllWithCategoriesAsync();
    }
}
