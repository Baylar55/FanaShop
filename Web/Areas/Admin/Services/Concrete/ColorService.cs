using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Color;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly ModelStateDictionary _modelState;

        public ColorService(IColorRepository colorRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _colorRepository = colorRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<ColorIndexVM> GetAllAsync()
        {
            var model = new ColorIndexVM
            {
                Colors = await _colorRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<ColorUpdateVM> GetUpdateModelAsync(int id)
        {
            var color = await _colorRepository.GetAsync(id);
            if (color == null) return null;
            var model = new ColorUpdateVM
            {
                Id = color.Id,
                Name = color.Name
            };
            return model;
        }

        public async Task<bool> CreateAsync(ColorCreateVM model)
        {
            bool isExist = await _colorRepository.AnyAsync(c => c.Name.ToLower().Trim() == model.Name.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }
            var color = new Color
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
            };
            await _colorRepository.CreateAsync(color);
            return true;
        }

        public async Task<bool> UpdateAsync(ColorUpdateVM model)
        {
            var color = await _colorRepository.GetAsync(model.Id);
            if (color != null)
            {
                bool isExist = await _colorRepository.AnyAsync(c => c.Name.ToLower().Trim() == model.Name.ToLower().Trim() && c.Id != model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "this title is already exist");
                    return false;
                }
                color.Name = model.Name;
                color.ModifiedAt = DateTime.Now;

                await _colorRepository.UpdateAsync(color);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var color = await _colorRepository.GetAsync(id);
            if (color != null)
            {
                await _colorRepository.DeleteAsync(color);
                return true;
            }
            return false;
        }
    }
}
