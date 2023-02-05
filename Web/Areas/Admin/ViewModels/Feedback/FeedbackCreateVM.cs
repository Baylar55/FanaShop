namespace Web.Areas.Admin.ViewModels.Feedback
{
    public class FeedbackCreateVM
    {
        public string ClientName { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
