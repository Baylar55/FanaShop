using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.ContactComponent;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ContactComponentService : IContactComponentService
    {
        private readonly IContactComponentRepository _contactComponentRepository;
        private readonly ModelStateDictionary _modelState;

        public ContactComponentService(IActionContextAccessor actionContextAccessor,
                                       IContactComponentRepository contactComponentRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _contactComponentRepository = contactComponentRepository;
        }

        public async Task<ContactComponentIndexVM> GetAsync()
        {
            var model = new ContactComponentIndexVM()
            {
                ContactComponents = await _contactComponentRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<ContactComponentUpdateVM> GetUpdateModelAsync(int id)
        {
            var contactComponent = await _contactComponentRepository.GetAsync(id);
            if (contactComponent == null) return null;
            var model = new ContactComponentUpdateVM()
            {
                Title = contactComponent.Title,
                Description = contactComponent.Description,
                IconCode = contactComponent.IconCode,
            };
            return model;
        }

        public async Task<bool> CreateAsync(ContactComponentCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var contactComponent = new ContactComponent
            {
                Title = model.Title,
                Description = model.Description,
                IconCode = model.IconCode,
                CreatedAt = DateTime.Now,
            };
            await _contactComponentRepository.CreateAsync(contactComponent);
            return true;
        }

        public async Task<bool> UpdateAsync(ContactComponentUpdateVM model, int id)
        {
            var contactComponent = await _contactComponentRepository.GetAsync(id);
            if (contactComponent == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            contactComponent.Title = model.Title;
            contactComponent.Description = model.Description;
            contactComponent.IconCode = model.IconCode;
            contactComponent.ModifiedAt = DateTime.Now;
            await _contactComponentRepository.UpdateAsync(contactComponent);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contactComponent = await _contactComponentRepository.GetAsync(id);
            if (contactComponent == null) return false;
            await _contactComponentRepository.DeleteAsync(contactComponent);
            return true;
        }
    }
}
