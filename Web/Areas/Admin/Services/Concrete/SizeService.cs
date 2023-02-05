using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Services.Concrete
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly ModelStateDictionary _modelState;

        public SizeService(ISizeRepository SizeRepository, IActionContextAccessor actionContextAccessor)
        {
            _sizeRepository = SizeRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<SizeIndexVM> GetAllAsync()
        {
            var model = new SizeIndexVM
            {
                Sizes = await _sizeRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<SizeUpdateVM> GetUpdateModelAsync(int id)
        {
            var Size = await _sizeRepository.GetAsync(id);
            if (Size == null) return null;
            var model = new SizeUpdateVM
            {
                Id = Size.Id,
                Title = Size.Title
            };
            return model;
        }

        public async Task<bool> CreateAsync(SizeCreateVM model)
        {
            bool isExist = await _sizeRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var Size = new Size
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _sizeRepository.CreateAsync(Size);
            return true;
        }

        public async Task<bool> UpdateAsync(SizeUpdateVM model)
        {
            var Size = await _sizeRepository.GetAsync(model.Id);
            if (Size != null)
            {
                bool isExist = await _sizeRepository.AnyAsync(c => c.Title.ToLower().Trim() == model.Title.ToLower().Trim() && c.Id != model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                Size.Title = model.Title;
                Size.ModifiedAt = DateTime.Now;

                await _sizeRepository.UpdateAsync(Size);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Size = await _sizeRepository.GetAsync(id);
            if (Size != null)
            {
                await _sizeRepository.DeleteAsync(Size);
                return true;
            }
            return false;
        }
    }
}
