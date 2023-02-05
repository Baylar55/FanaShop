using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.TeamMember;

namespace Web.Areas.Admin.Services.Concrete
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IFileService _fileService;

        public TeamMemberService(IActionContextAccessor actionContextAccessor,
                                ITeamMemberRepository teamMemberRepository,
                                IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _teamMemberRepository = teamMemberRepository;
            _fileService = fileService;
        }

        public async Task<TeamMemberIndexVM> GetAsync()
        {
            var model = new TeamMemberIndexVM()
            {
                TeamMembers = await _teamMemberRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<TeamMemberUpdateVM> GetUpdateModelAsync(int id)
        {
            var dbTeamMember = await _teamMemberRepository.GetAsync(id);
            if (dbTeamMember == null) return null;
            var model = new TeamMemberUpdateVM()
            {
                Name = dbTeamMember.Name,
                Speciality = dbTeamMember.Speciality,
            };
            return model;
        }

        public async Task<bool> CreateAsync(TeamMemberCreateVM model)
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
            var teamMember = new TeamMember
            {
                Name = model.Name,
                Speciality = model.Speciality,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _teamMemberRepository.CreateAsync(teamMember);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teamMember = await _teamMemberRepository.GetAsync(id);
            if (teamMember == null) return false;
            _fileService.Delete(teamMember.PhotoName);
            await _teamMemberRepository.DeleteAsync(teamMember);
            return true;
        }

        public async Task<bool> UpdateAsync(TeamMemberUpdateVM model, int id)
        {
            var teamMember = await _teamMemberRepository.GetAsync(id);
            if (teamMember == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            teamMember.Name = model.Name;
            teamMember.Speciality = model.Speciality;
            teamMember.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(teamMember.PhotoName);
                teamMember.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _teamMemberRepository.UpdateAsync(teamMember);
            return true;
        }
    }
}
