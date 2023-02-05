using Core.Entities;

namespace Web.ViewModels.Shop
{
    public class SingleProductIndexVM
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}
