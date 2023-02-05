using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ModelStateDictionary _modelState;

        public BrandService(IBrandRepository brandRepository,
                                      IActionContextAccessor actionContextAccessor)
        {
            _brandRepository = brandRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<BrandIndexVM> GetAllAsync()
        {
            var model = new BrandIndexVM
            {
                Brands = await _brandRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<BrandUpdateVM> GetUpdateModelAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand == null) return null;
            var model = new BrandUpdateVM
            {
                Id = brand.Id,
                Title = brand.Title
            };
            return model;
        }

        public async Task<bool> CreateAsync(BrandCreateVM model)
        {
            bool isExist = await _brandRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var brand = new Brand
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _brandRepository.CreateAsync(brand);
            return true;
        }

        public async Task<bool> UpdateAsync(BrandUpdateVM model)
        {
            var brand = await _brandRepository.GetAsync(model.Id);
            if (brand != null)
            {
                bool isExist = await _brandRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim() && c.Id != model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                brand.Title = model.Title;
                brand.ModifiedAt = DateTime.Now;

                await _brandRepository.UpdateAsync(brand);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand != null)
            {
                await _brandRepository.DeleteAsync(brand);
                return true;
            }
            return false;
        }

    }
}
