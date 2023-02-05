using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Characteristic;

namespace Web.Areas.Admin.Services.Concrete
{
    public class CharacteristicService : ICharacteristicService
    {
        private readonly ICharacteristicRepository _characteristicRepository;
        private readonly ModelStateDictionary _modelState;

        public CharacteristicService(IActionContextAccessor actionContextAccessor,
                                  ICharacteristicRepository characteristicRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<CharacteristicIndexVM> GetAsync()
        {
            var model = new CharacteristicIndexVM()
            {
                Characteristics = await _characteristicRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<CharacteristicUpdateVM> GetUpdateModelAsync(int id)
        {
            var characteristic = await _characteristicRepository.GetAsync(id);
            if (characteristic == null) return null;
            var model = new CharacteristicUpdateVM()
            {
                Title = characteristic.Title,
                Description = characteristic.Description,
                IconCode = characteristic.IconCode,
            };
            return model;
        }

        public async Task<bool> CreateAsync(CharacteristicCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var characteristic = new Characteristic
            {
                Title = model.Title,
                Description = model.Description,
                IconCode = model.IconCode,
                CreatedAt = DateTime.Now,
            };
            await _characteristicRepository.CreateAsync(characteristic);
            return true;
        }

        public async Task<bool> UpdateAsync(CharacteristicUpdateVM model, int id)
        {
            var characteristic = await _characteristicRepository.GetAsync(id);
            if (characteristic == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            characteristic.Title = model.Title;
            characteristic.Description = model.Description;
            characteristic.IconCode = model.IconCode;
            characteristic.ModifiedAt = DateTime.Now;
            await _characteristicRepository.UpdateAsync(characteristic);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var characteristic = await _characteristicRepository.GetAsync(id);
            if (characteristic == null) return false;
            await _characteristicRepository.DeleteAsync(characteristic);
            return true;
        }
    }
}
