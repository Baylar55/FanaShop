using DataAccess.Repositories.Abstract;
using Newtonsoft.Json;
using Web.Services.Abstract;
using Web.ViewModels.Wishlist;


namespace Web.Services.Concrete
{
    public class WishlistService : IWishlistService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public WishlistService(IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }
        public async Task<bool> WishlistAddAsync(WishlistAddVM model)
        {
            List<WishlistAddVM> wishlist;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistAddVM>();
            }

            var wishlistProduct = wishlist.Find(f => f.Id == model.Id);
            if (wishlistProduct == null)
            {
                wishlist.Add(model);
                var serializedItem = JsonConvert.SerializeObject(wishlist);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("Wishlist", serializedItem);
                return true;
            }
            return false;
        }

        public async Task<List<WishlistItemVM>> GetAllAsync()
        {
            List<WishlistAddVM> wishlistItems;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"] != null)
            {
                wishlistItems = JsonConvert.DeserializeObject<List<WishlistAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"]);
                List<WishlistItemVM> model = new List<WishlistItemVM>();

                foreach (var Wishlist in wishlistItems)
                {
                    var dbitem = await _productRepository.GetProductDetailsAsync(Wishlist.Id);
                    if (dbitem != null)
                    {
                        model.Add(new WishlistItemVM
                        {
                            Id = dbitem.Id,
                            PhotoName = dbitem.MainPhoto,
                            Price = dbitem.Price,
                            Title = dbitem.Name
                        });
                    }
                }
                return model;
            }
            return null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            List<WishlistAddVM> wishlist;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"] == null) return false;
            wishlist = JsonConvert.DeserializeObject<List<WishlistAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["Wishlist"]);

            foreach (var item in wishlist)
            {
                if (item.Id == id)
                {
                    wishlist.Remove(item);
                    var serializedItem = JsonConvert.SerializeObject(wishlist);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("Wishlist", serializedItem);
                    return true;
                }
            }
            return true;
        }
    }
}
