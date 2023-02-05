using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IStyleGalleryRepository _styleGalleryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITrendRepository _trendRepository;

        public HomeService(IHomeMainSliderRepository homeMainSliderRepository,
                           IStyleGalleryRepository styleGalleryRepository,
                           IProductRepository productRepository,
                           ITrendRepository trendRepository)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _styleGalleryRepository = styleGalleryRepository;
            _productRepository = productRepository;
            _trendRepository = trendRepository;
        }
        public async Task<HomeIndexVM> GetAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                BestSellerProducts = await _productRepository.GetBestSellerAsync(),
                StyleGalleries = await _styleGalleryRepository.GetAllAsync(),
                Trends= await _trendRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
