using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Trend;

namespace Web.Areas.Admin.Services.Concrete
{
    public class TrendService : ITrendService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        private readonly ITrendRepository _trendRepository;
        public TrendService(IActionContextAccessor actionContextAccessor,
                                       IFileService fileService,
                                       ITrendRepository TrendRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _trendRepository = TrendRepository;
        }

        public async Task<TrendIndexVM> GetAsync()
        {
            var model = new TrendIndexVM()
            {
                Trends = await _trendRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<TrendUpdateVM> GetUpdateModelAsync(int id)
        {
            var trend = await _trendRepository.GetAsync(id);
            if (trend == null) return null;
            var model = new TrendUpdateVM()
            {
                Title = trend.Title,
                SubTitle = trend.SubTitle,
                Description = trend.Description,
            };
            return model;
        }

        public async Task<bool> CreateAsync(TrendCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            bool isExist = await _trendRepository.AnyAsync(d => d.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title is already exist");
                return false;
            }
            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photo", "Photo should be in image format");
                return false;
            }
            else if (!_fileService.CheckSize(model.Photo, 400))
            {
                _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                return false;
            }
            var trend = new Trend
            {
                Title = model.Title,
                SubTitle = model.SubTitle,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _trendRepository.CreateAsync(trend);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trend = await _trendRepository.GetAsync(id);
            if (trend == null) return false;
            _fileService.Delete(trend.PhotoName);
            await _trendRepository.DeleteAsync(trend);
            return true;
        }

        public async Task<bool> UpdateAsync(TrendUpdateVM model, int id)
        {
            var trend = await _trendRepository.GetAsync(id);
            if (trend == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            bool isExist = await _trendRepository.AnyAsync(d => d.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title is already exist");
                return false;
            }
            trend.Title = model.Title;
            trend.SubTitle = model.SubTitle;
            trend.Description = model.Description;
            trend.ModifiedAt = DateTime.Now;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Photo should be in image format");
                    return false;
                }
                else if (!_fileService.CheckSize(model.Photo, 400))
                {
                    _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                    return false;
                }
                _fileService.Delete(trend.PhotoName);
                trend.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _trendRepository.UpdateAsync(trend);
            return true;
        }
    }
}
