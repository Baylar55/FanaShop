using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IAboutIntroRepository : IRepository<AboutIntro>
    {
        Task<AboutIntro> GetAsync();
    }
}
