using Core.Entities;

namespace Web.Areas.Admin.ViewModels.Blog
{
    public class BlogDetailsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhotoName { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string QuoteAuthor { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<BlogPhoto> BlogPhotos { get; set; }
    }
}
