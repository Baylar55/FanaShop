namespace Web.Areas.Admin.ViewModels.StyleGallery
{
    public class StyleGalleryCreateVM
    {
        public string CoverLetter { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
    }
}
