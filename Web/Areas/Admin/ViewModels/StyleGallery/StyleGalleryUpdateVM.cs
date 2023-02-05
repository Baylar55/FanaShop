namespace Web.Areas.Admin.ViewModels.StyleGallery
{
    public class StyleGalleryUpdateVM
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string CoverLetter { get; set; }
        public IFormFile Photo { get; set; }
    }
}
