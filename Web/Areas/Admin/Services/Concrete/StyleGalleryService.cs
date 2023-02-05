using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.StyleGallery;

namespace Web.Areas.Admin.Services.Concrete
{
    public class StyleGalleryService:IStyleGalleryService
    { 
        private readonly ModelStateDictionary _modelState;
        private readonly IStyleGalleryRepository _styleGalleryRepository;
        private readonly IFileService _fileService;

        public StyleGalleryService(IActionContextAccessor actionContextAccessor,
                                IStyleGalleryRepository StyleGalleryRepository,
                                IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _styleGalleryRepository = StyleGalleryRepository;
            _fileService = fileService;
        }

        public async Task<StyleGalleryIndexVM> GetAsync()
        {
            var model = new StyleGalleryIndexVM()
            {
                StyleGallerys = await _styleGalleryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<StyleGalleryUpdateVM> GetUpdateModelAsync(int id)
        {
            var dbStyleGallery = await _styleGalleryRepository.GetAsync(id);
            if (dbStyleGallery == null) return null;
            var model = new StyleGalleryUpdateVM()
            {
               CoverLetter = dbStyleGallery.CoverLetter,
               Order= dbStyleGallery.Order,
            };
            return model;
        }

        public async Task<bool> CreateAsync(StyleGalleryCreateVM model)
        {
            var galleries= await _styleGalleryRepository.GetAllAsync();
            var galleryCount= galleries.Count();
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
            var StyleGallery = new StyleGallery
            {
                CoverLetter = model.CoverLetter,
                Order = galleryCount++,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _styleGalleryRepository.CreateAsync(StyleGallery);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var StyleGallery = await _styleGalleryRepository.GetAsync(id);
            if (StyleGallery == null) return false;
            _fileService.Delete(StyleGallery.PhotoName);
            await _styleGalleryRepository.DeleteAsync(StyleGallery);
            return true;
        }

        public async Task<bool> UpdateAsync(StyleGalleryUpdateVM model, int id)
        {
            var StyleGallery = await _styleGalleryRepository.GetAsync(id);
            if (StyleGallery == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            StyleGallery.CoverLetter = model.CoverLetter;
            StyleGallery.Order = model.Order;
            StyleGallery.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(StyleGallery.PhotoName);
                StyleGallery.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _styleGalleryRepository.UpdateAsync(StyleGallery);
            return true;
        }
    }
}
