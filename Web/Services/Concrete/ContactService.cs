using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Contact;

namespace Web.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IContactComponentRepository _contactComponentRepository;

        public ContactService(ILocationRepository locationRepository,
                              IContactComponentRepository contactComponentRepository)
        {
            _locationRepository = locationRepository;
            _contactComponentRepository = contactComponentRepository;
        }

        public async Task<ContactIndexVM> GetAsync()
        {
            var model = new ContactIndexVM
            {
                Location = await _locationRepository.GetAsync(),
                ContactComponents = await _contactComponentRepository.GetAllAsync()
            };
            return model;
        }
    }
}
