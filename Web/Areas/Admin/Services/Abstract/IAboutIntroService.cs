using Web.Areas.Admin.ViewModels.AboutIntro;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IAboutIntroService
    {

        Task<AboutIntroIndexVM> GetAsync();
        Task<AboutIntroUpdateVM> GetUpdateModelAsync();
        Task<bool> CreateAsync(AboutIntroCreateVM model);
        Task<bool> UpdateAsync(AboutIntroUpdateVM model);
        Task<bool> DeleteAsync();
    }
}
