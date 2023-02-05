using Core.Entities.Base;

namespace Core.Entities
{
    public class PriceRange : BaseEntity
    {
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int Step { get; set; }
    }
}
