using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class FeatureViewComponent : ViewComponent
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureViewComponent(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _featureRepository.GetAllAsync();
            return View(features);
        }
    }
}
