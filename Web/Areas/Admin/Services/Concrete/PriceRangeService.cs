using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.PriceRange;

namespace Web.Areas.Admin.Services.Concrete
{
    public class PriceRangeService : IPriceRangeService
    {
        private readonly IPriceRangeRepository _priceRangeRepository;
        private readonly ModelStateDictionary _modelState;

        public PriceRangeService(IPriceRangeRepository PriceRangeRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _priceRangeRepository = PriceRangeRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<PriceRangeIndexVM> GetAllAsync()
        {
            var model = new PriceRangeIndexVM
            {
                PriceRange = await _priceRangeRepository.GetAsync()
            };
            return model;
        }

        public async Task<PriceRangeUpdateVM> GetUpdateModelAsync(int id)
        {
            var priceRange = await _priceRangeRepository.GetAsync(id);
            if (priceRange == null) return null;
            var model = new PriceRangeUpdateVM
            {
                Id = priceRange.Id,
                MinPrice = priceRange.MinPrice,
                MaxPrice = priceRange.MaxPrice,
                Step = priceRange.Step,
            };
            return model;
        }

        public async Task<bool> CreateAsync(PriceRangeCreateVM model)
        {
            if (model.MaxPrice <= model.MinPrice)
            {
                _modelState.AddModelError("MaxPrice", "Maximum price can't be lower than minimum price");
                return false;
            }
            if (model.Step > model.MaxPrice)
            {
                _modelState.AddModelError("Step", "Step can't be more than maximum price");
                return false;
            }
            var priceRange = new PriceRange
            {
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice,
                Step = model.Step,
                CreatedAt = DateTime.Now,
            };
            await _priceRangeRepository.CreateAsync(priceRange);
            return true;
        }

        public async Task<bool> UpdateAsync(PriceRangeUpdateVM model)
        {
            var PriceRange = await _priceRangeRepository.GetAsync(model.Id);
            if (model.MaxPrice <= model.MinPrice)
            {
                _modelState.AddModelError("MaxPrice", "Maximum price can't be lower than minimum price");
                return false;
            }
            if (model.Step > model.MaxPrice)
            {
                _modelState.AddModelError("Step", "Step can't be more than maximum price");
                return false;
            }
            if (PriceRange != null)
            {
                PriceRange.MinPrice = model.MinPrice;
                PriceRange.MaxPrice = model.MaxPrice;
                PriceRange.Step = model.Step;
                PriceRange.ModifiedAt = DateTime.Now;

                await _priceRangeRepository.UpdateAsync(PriceRange);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var PriceRange = await _priceRangeRepository.GetAsync(id);
            if (PriceRange != null)
            {
                await _priceRangeRepository.DeleteAsync(PriceRange);
                return true;
            }
            return false;
        }
    }
}
