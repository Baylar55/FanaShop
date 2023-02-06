using Core.Entities;

namespace Web.ViewModels.Pages
{
    public class PagesIndexVM
    {
        public List<FAQCategory> FAQCategories { get; set; }
        public List<Question> Questions { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public AboutIntro AboutIntro { get; set; }
        public List<Blog> Blogs { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 3;
        public int PageCount { get; set; }
    }
}
