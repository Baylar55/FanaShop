using Web.Areas.Admin.ViewModels.TeamMember;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ITeamMemberService
    {
        Task<TeamMemberIndexVM> GetAsync();
        Task<TeamMemberUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> CreateAsync(TeamMemberCreateVM model);
        Task<bool> UpdateAsync(TeamMemberUpdateVM model, int id);
        Task<bool> DeleteAsync(int id);
    }
}
