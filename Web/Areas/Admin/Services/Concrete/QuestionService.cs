using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IQuestionRepository _questionRepository;
        private readonly IFAQCategoryRepository _categoryRepository;
        private readonly IFileService _fileService;

        public QuestionService(IActionContextAccessor actionContextAccessor,
                               IQuestionRepository ourVisionRepository,
                               IFAQCategoryRepository categoryRepository,
                               IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _questionRepository = ourVisionRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
        }

        public async Task<QuestionIndexVM> GetAsync()
        {
            var model = new QuestionIndexVM()
            {
                Questions = await _questionRepository.GetAllWithCategoriesAsync()
            };
            return model;
        }

        public async Task<QuestionUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAllAsync();
            var question = await _questionRepository.GetAsync(id);
            if (question == null) return null;
            var model = new QuestionUpdateVM()
            {
                Title = question.Title,
                Answer = question.Answer,
                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<QuestionCreateVM> GetCreateModelAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            var model = new QuestionCreateVM
            {
                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<bool> CreateAsync(QuestionCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var category = await _categoryRepository.GetAllAsync();
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
            var question = new Question
            {
                Title = model.Title,
                Answer = model.Answer,
                FAQCategoryId = model.FAQCategoryId,
                CreatedAt = DateTime.Now,
            };
            await _questionRepository.CreateAsync(question);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _questionRepository.GetAsync(id);
            if (question == null) return false;
            await _questionRepository.DeleteAsync(question);
            return true;
        }

        public async Task<bool> UpdateAsync(QuestionUpdateVM model, int id)
        {
            var question = await _questionRepository.GetAsync(id);
            if (question == null) return false;
            if (!_modelState.IsValid) return false;
            var category = await _categoryRepository.GetAllAsync();
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
            if (model == null) return false;
            question.Title = model.Title;
            question.Answer = model.Answer;
            question.ModifiedAt = DateTime.Now;

            await _questionRepository.UpdateAsync(question);
            return true;
        }
    }
}
