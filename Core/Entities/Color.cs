using Core.Entities.Base;

namespace Core.Entities
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
    }
}
