using Core.Entities.Base;

namespace Core.Entities
{
    public class Brand : BaseEntity
    {
        public string Title { get; set; }
        public List<Product> Products { get; set; }
    }
}
