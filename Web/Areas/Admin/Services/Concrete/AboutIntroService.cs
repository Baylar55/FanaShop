using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.AboutIntro;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AboutIntroService : IAboutIntroService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IAboutIntroRepository _aboutIntroRepository;
        private readonly IFileService _fileService;

        public AboutIntroService(IActionContextAccessor actionContextAccessor,
                                IAboutIntroRepository aboutIntroRepository,
                                IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _aboutIntroRepository = aboutIntroRepository;
            _fileService = fileService;
        }

        public async Task<AboutIntroIndexVM> GetAsync()
        {
            var model = new AboutIntroIndexVM()
            {
                AboutIntro = await _aboutIntroRepository.GetAsync()
            };
            return model;
        }

        public async Task<AboutIntroUpdateVM> GetUpdateModelAsync()
        {
            var dbAboutIntro = await _aboutIntroRepository.GetAsync();
            if (dbAboutIntro == null) return null;
            var model = new AboutIntroUpdateVM()
            {
                Title = dbAboutIntro.Title,
                Description = dbAboutIntro.Description,
            };
            return model;
        }

        public async Task<bool> CreateAsync(AboutIntroCreateVM model)
        {
            if (!_modelState.IsValid) return false;
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
            var aboutIntro = new AboutIntro
            {
                Title = model.Title,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _aboutIntroRepository.CreateAsync(aboutIntro);
            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var aboutIntro = await _aboutIntroRepository.GetAsync();
            if (aboutIntro == null) return false;
            _fileService.Delete(aboutIntro.PhotoName);
            await _aboutIntroRepository.DeleteAsync(aboutIntro);
            return true;
        }

        public async Task<bool> UpdateAsync(AboutIntroUpdateVM model)
        {
            var aboutIntro = await _aboutIntroRepository.GetAsync();
            if (aboutIntro == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            aboutIntro.Title = model.Title;
            aboutIntro.Description = model.Description;
            aboutIntro.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(aboutIntro.PhotoName);
                aboutIntro.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _aboutIntroRepository.UpdateAsync(aboutIntro);
            return true;
        }
    }
}
