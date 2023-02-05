using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Feedback;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IFileService _fileService;

        public FeedbackService(IActionContextAccessor actionContextAccessor,
                                IFeedbackRepository feedbackRepository,
                                IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _feedbackRepository = feedbackRepository;
            _fileService = fileService;
        }

        public async Task<FeedbackIndexVM> GetAsync()
        {
            var model = new FeedbackIndexVM()
            {
                Feedbacks = await _feedbackRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<FeedbackUpdateVM> GetUpdateModelAsync(int id)
        {
            var dbFeedback = await _feedbackRepository.GetAsync(id);
            if (dbFeedback == null) return null;
            var model = new FeedbackUpdateVM()
            {
                ClientName = dbFeedback.ClientName,
                Description = dbFeedback.Description,
            };
            return model;
        }

        public async Task<bool> CreateAsync(FeedbackCreateVM model)
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
            var feedback = new Feedback
            {
                ClientName = model.ClientName,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                CreatedAt = DateTime.Now,
            };
            await _feedbackRepository.CreateAsync(feedback);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feedback = await _feedbackRepository.GetAsync(id);
            if (feedback == null) return false;
            _fileService.Delete(feedback.PhotoName);
            await _feedbackRepository.DeleteAsync(feedback);
            return true;
        }

        public async Task<bool> UpdateAsync(FeedbackUpdateVM model, int id)
        {
            var feedback = await _feedbackRepository.GetAsync(id);
            if (feedback == null) return false;
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            feedback.ClientName = model.ClientName;
            feedback.Description = model.Description;
            feedback.ModifiedAt = DateTime.Now;

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
                _fileService.Delete(feedback.PhotoName);
                feedback.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _feedbackRepository.UpdateAsync(feedback);
            return true;
        }
    }
}
