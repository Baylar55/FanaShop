using Core.Entities.Base;

namespace Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhoto { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string QuoteAuthor { get; set; }
        public ICollection<BlogPhoto> BlogPhotos { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
