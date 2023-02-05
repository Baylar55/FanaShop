using Core.Entities;

namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<HomeMainSlider> HomeMainSliders { get; set; }
        public List<Product> BestSellerProducts { get; set; }
        public List<Feature> Features { get; set; }
        public List<Trend> Trends { get; set; }
        public List<StyleGallery> StyleGalleries { get; set; }
    }
}
