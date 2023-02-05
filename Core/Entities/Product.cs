using Core.Constants;
using Core.Entities.Base;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string MainPhoto { get; set; }
        public string BackPhoto { get; set; }
        public ProductStatus? Status { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
        public ICollection<ProductColor>? Colors { get; set; }
        public ICollection<ProductSize>? Sizes { get; set; }
        public bool BestSeller { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
