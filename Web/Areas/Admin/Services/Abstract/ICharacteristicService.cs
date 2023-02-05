using Web.Areas.Admin.ViewModels.Characteristic;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ICharacteristicService
    {
        Task<CharacteristicIndexVM> GetAsync();
        Task<CharacteristicUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(CharacteristicCreateVM model);
        Task<bool> UpdateAsync(CharacteristicUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
