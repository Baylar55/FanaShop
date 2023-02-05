using Web.Areas.Admin.ViewModels.ContactComponent;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IContactComponentService
    {
        Task<ContactComponentIndexVM> GetAsync();
        Task<ContactComponentUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(ContactComponentCreateVM model);
        Task<bool> UpdateAsync(ContactComponentUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
