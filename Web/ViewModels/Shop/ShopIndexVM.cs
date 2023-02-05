using Core.Entities;

namespace Web.ViewModels.Shop
{
    public class ShopIndexVM
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public PriceRange PriceRange { get; set; }
        public List<Product> Products { get; set; }
        public List<Size> Sizes { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 6;
        public int PageCount { get; set; }
    }
}
