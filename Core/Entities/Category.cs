using Core.Entities.Base;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
