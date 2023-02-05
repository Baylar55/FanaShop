namespace Web.Areas.Admin.ViewModels.Feedback
{
    public class FeedbackUpdateVM
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
