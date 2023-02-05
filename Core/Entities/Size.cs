using Core.Entities.Base;

namespace Core.Entities
{
    public class Size : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
