using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Services.Concrete
{
    public class LocationService : ILocationService
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ILocationRepository _locationRepository;
        private readonly ModelStateDictionary _modelState;

        public LocationService(IActionContextAccessor actionContextAccessor,
                               ILocationRepository locationRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _locationRepository = locationRepository;
        }

        public async Task<LocationIndexVM> GetAsync()
        {
            var model = new LocationIndexVM()
            {
                Location = await _locationRepository.GetAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(LocationCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var location = new Location
            {
                EmbedCode = model.EmbedCode,
                CreatedAt = DateTime.Now,
            };
            await _locationRepository.CreateAsync(location);
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var location = await _locationRepository.GetAsync();
            if (location == null) return false;
            await _locationRepository.DeleteAsync(location);
            return true;
        }

        public async Task<LocationUpdateVM> GetUpdateModelAsync()
        {
            var dblocation = await _locationRepository.GetAsync();
            if (dblocation == null) return null;
            var model = new LocationUpdateVM()
            {
                EmbedCode = dblocation.EmbedCode,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(LocationUpdateVM model)
        {
            var location = await _locationRepository.GetAsync();
            if (location == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            location.EmbedCode = model.EmbedCode;
            location.ModifiedAt = DateTime.Now;
            await _locationRepository.UpdateAsync(location);
            return true;
        }
    }
}
