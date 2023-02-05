using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string QuoteAuthor { get; set; }
        public IFormFile CoverPhoto { get; set; }
        public ICollection<IFormFile> Photos { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
