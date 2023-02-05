namespace Web.Areas.Admin.ViewModels.AboutIntro
{
    public class AboutIntroUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
