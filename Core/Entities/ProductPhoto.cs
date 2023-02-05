using Core.Entities.Base;

namespace Core.Entities
{
    public class ProductPhoto : BaseEntity
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
