using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Shop;

namespace Web.Services.Concrete
{
    public class ShopService : IShopService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IPriceRangeRepository _priceRangeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopService(ICategoryRepository categoryRepository,
                           IBrandRepository brandRepository,
                           IColorRepository colorRepository,
                           IProductRepository productRepository,
                           ISizeRepository sizeRepository,
                           IPriceRangeRepository priceRangeRepository,
                           IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _priceRangeRepository = priceRangeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ShopIndexVM> GetAsync(ShopIndexVM model)
        {
            var queryString = _httpContextAccessor.HttpContext.Request.Query;
            string productName = queryString["Name"];
            string categoryName = queryString["category-filter"];
            string brandName = queryString["brand-filter"];
            string size = queryString["size-filter"];
            string color = queryString["color-filter"];
            string minPrice = queryString["minprice-filter"];
            string maxPrice = queryString["maxprice-filter"];

            model.Name = productName;
            model.CategoryName = categoryName;
            model.BrandName = brandName;

            if (size != null) model.SizeId = int.Parse(size);
            if (color != null) model.ColorId = int.Parse(color);
            if (minPrice != null) model.MinPrice = int.Parse(minPrice);
            if (maxPrice != null) model.MaxPrice = int.Parse(maxPrice);

            var products = _productRepository.FilterByName(model.Name);
            products = await _productRepository.FilterByPrice(products, model.MinPrice, model.MaxPrice);
            products = _productRepository.FilterByCategory(model.CategoryName, products);
            products = _productRepository.FilterByBrand(model.BrandName, products);
            products = _productRepository.FilterBySize(model.SizeId, products);
            products = _productRepository.FilterByColor(model.ColorId, products);
            var pageCount = await _productRepository.GetPageCountAsync(model.Take, products);
            products = await _productRepository.PaginateProductsAsync(model.Page, model.Take, products);

            model = new ShopIndexVM()
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync(),
                Colors = await _colorRepository.GetAllAsync(),
                PriceRange = await _priceRangeRepository.GetAsync(),
                Products = await products.ToListAsync(),
                Sizes = await _sizeRepository.GetAllAsync(),
                Name = model.Name,
                PageCount = pageCount,
                Page = model.Page,
            };
            return model;
        }

        public async Task<List<Product>> LoadProductsAsync(int id)
        {
            return await _productRepository.GetAllByCategoryAsync(id);
        }

        public async Task<SingleProductIndexVM> GetProductByIdAsync(int id)
        {
            var model = new SingleProductIndexVM()
            {
                Product = await _productRepository.GetProductDetailsAsync(id),
                RelatedProducts = await _productRepository.GetRelatedAsync(id),
            };
            return model;
        }
    }
}

