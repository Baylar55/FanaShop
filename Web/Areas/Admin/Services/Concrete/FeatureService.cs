using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Feature;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly ModelStateDictionary _modelState;

        public FeatureService(IActionContextAccessor actionContextAccessor,
                                  IFeatureRepository featureRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _featureRepository = featureRepository;
        }

        public async Task<FeatureIndexVM> GetAsync()
        {
            var model = new FeatureIndexVM()
            {
                Features = await _featureRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<FeatureUpdateVM> GetUpdateModelAsync(int id)
        {
            var feature = await _featureRepository.GetAsync(id);
            if (feature == null) return null;
            var model = new FeatureUpdateVM()
            {
                Title = feature.Title,
                Description = feature.Description,
                IconCode = feature.IconCode,
            };
            return model;
        }

        public async Task<bool> CreateAsync(FeatureCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var feature = new Feature
            {
                Title = model.Title,
                Description = model.Description,
                IconCode = model.IconCode,
                CreatedAt = DateTime.Now,
            };
            await _featureRepository.CreateAsync(feature);
            return true;
        }

        public async Task<bool> UpdateAsync(FeatureUpdateVM model, int id)
        {
            var feature = await _featureRepository.GetAsync(id);
            if (feature == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            feature.Title = model.Title;
            feature.Description = model.Description;
            feature.IconCode = model.IconCode;
            feature.ModifiedAt = DateTime.Now;
            await _featureRepository.UpdateAsync(feature);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feature = await _featureRepository.GetAsync(id);
            if (feature == null) return false;
            await _featureRepository.DeleteAsync(feature);
            return true;
        }
    }
}
