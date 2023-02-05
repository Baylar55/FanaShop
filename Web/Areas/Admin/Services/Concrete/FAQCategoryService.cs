using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.FAQCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FAQCategoryService : IFAQCategoryService
    {
        private readonly IFAQCategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;

        public FAQCategoryService(IFAQCategoryRepository categoryRepository,
                                 IActionContextAccessor actionContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<FAQCategoryIndexVM> GetAllAsync()
        {
            var model = new FAQCategoryIndexVM
            {
                FAQCategories = await _categoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<FAQCategoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null) return null;
            var model = new FAQCategoryUpdateVM
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
            };
            return model;
        }

        public async Task<bool> CreateAsync(FAQCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var category = new FAQCategory
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
            };
            await _categoryRepository.CreateAsync(category);
            return true;
        }

        public async Task<bool> UpdateAsync(FAQCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var category = await _categoryRepository.GetAsync(model.Id);
            if (category != null)
            {
                bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim() && model.Id != c.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                category.Title = model.Title;
                category.Description = model.Description;
                category.ModifiedAt = DateTime.Now;

                await _categoryRepository.UpdateAsync(category);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
                return true;
            }
            return false;
        }
    }
}
