using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;

namespace DataAccess.Repositories.Concrete
{
    public class CharacteristicRepository : Repository<Characteristic>, ICharacteristicRepository
    {
        public CharacteristicRepository(AppDbContext context) : base(context)
        {
        }
    }
}
