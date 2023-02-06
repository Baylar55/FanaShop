using Core.Entities.Base;

namespace Core.Entities
{
    public class BasketProduct : BaseEntity
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
