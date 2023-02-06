using Core.Entities;
using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Pages;

namespace Web.Services.Concrete
{
    public class PageService : IPageService
    {
        private readonly IFAQCategoryRepository _categoryRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAboutIntroRepository _aboutIntroRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly ICharacteristicRepository _characteristicRepository;

        public PageService(
                           IFAQCategoryRepository categoryRepository,
                           IQuestionRepository questionRepository,
                           ITeamMemberRepository teamMemberRepository,
                           IFeedbackRepository feedbackRepository,
                           IAboutIntroRepository aboutIntroRepository,
                           IBlogRepository blogRepository,
                           ICharacteristicRepository characteristicRepository)
        {

            _categoryRepository = categoryRepository;
            _questionRepository = questionRepository;
            _teamMemberRepository = teamMemberRepository;
            _feedbackRepository = feedbackRepository;
            _aboutIntroRepository = aboutIntroRepository;
            _blogRepository = blogRepository;
            _characteristicRepository = characteristicRepository;
        }

        public async Task<PagesIndexVM> GetAsync(PagesIndexVM model)
        {
            var pageCount = await _blogRepository.GetPageCountAsync(model.Take);

            if (model.Page <= 0) return model;

            var blogs = await _blogRepository.PaginateBlogsAsync(model.Page, model.Take);
            model = new PagesIndexVM()
            {
                FAQCategories = await _categoryRepository.GetAllAsync(),
                Questions = await _questionRepository.GetAllWithCategoriesAsync(),
                TeamMembers = await _teamMemberRepository.GetAllAsync(),
                Feedbacks = await _feedbackRepository.GetAllAsync(),
                Characteristics = await _characteristicRepository.GetAllAsync(),
                AboutIntro = await _aboutIntroRepository.GetAsync(),
                Blogs = blogs,
                Take= model.Take,
                PageCount= pageCount,
                Page=model.Page,
            };
            return model;
        }

        public async Task<SingleBlogIndexVM> GetBlogByIdAsync(int id)
        {
            var model = new SingleBlogIndexVM()
            {
                Blog = await _blogRepository.GetWithPhotosAsync(id),
                Blogs = await _blogRepository.GetRelatedAsync(id),
            };
            return model;
        }

        public async Task<List<Question>> LoadQuestionsAsync(int id)
        {
            return await _questionRepository.GetAllByCategoryAsync(id);
        }
    }
}
