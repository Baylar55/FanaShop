using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;

        public HomeMainSliderService(IActionContextAccessor actionContextAccessor,
                                     IFileService fileService,
                                     IHomeMainSliderRepository homeMainSliderRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _homeMainSliderRepository = homeMainSliderRepository;
        }

        public async Task<HomeMainSliderIndexVM> GetAsync()
        {
            var model = new HomeMainSliderIndexVM()
            {
                HomeMainSlider = await _homeMainSliderRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeMainSlider == null) return null;
            var model = new HomeMainSliderUpdateVM()
            {
                Title = homeMainSlider.Title,
                Description = homeMainSlider.Description,
                Order = homeMainSlider.Order,
            };
            return model;
        }

        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeMainSliderRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower());
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

            var mainSliders = await _homeMainSliderRepository.GetAllAsync();
            int order = mainSliders.Count();
            ++order;
            var homeMainSlider = new HomeMainSlider
            {
                Title = model.Title,
                Description = model.Description,
                Order = order++,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _homeMainSliderRepository.CreateAsync(homeMainSlider);
            return true;
        }

        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model, int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeMainSlider == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            var isExist = await _homeMainSliderRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower() && s.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This title is already exist");
                return false;
            }
            homeMainSlider.Title = model.Title;
            homeMainSlider.Description = model.Description;
            homeMainSlider.Order = model.Order;
            homeMainSlider.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(homeMainSlider.PhotoName);
                homeMainSlider.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _homeMainSliderRepository.UpdateAsync(homeMainSlider);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var homeMainSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeMainSlider == null) return false;
            _fileService.Delete(homeMainSlider.PhotoName);
            await _homeMainSliderRepository.DeleteAsync(homeMainSlider);
            return true;
        }
    }
}
