﻿using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Category;

namespace Web.Areas.Admin.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;

        public CategoryService(ICategoryRepository categoryRepository, IActionContextAccessor actionContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<CategoryIndexVM> GetAllAsync()
        {
            var model = new CategoryIndexVM
            {
                Categories = await _categoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<CategoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null) return null;
            var model = new CategoryUpdateVM
            {
                Id = category.Id,
                Title = category.Title
            };
            return model;
        }

        public async Task<bool> CreateAsync(CategoryCreateVM model)
        {
            bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var category = new Category
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _categoryRepository.CreateAsync(category);
            return true;
        }

        public async Task<bool> UpdateAsync(CategoryUpdateVM model)
        {
            var category = await _categoryRepository.GetAsync(model.Id);
            if (category != null)
            {
                bool isExist = await _categoryRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim() && c.Id != model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                category.Title = model.Title;
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