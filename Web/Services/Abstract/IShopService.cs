using Core.Entities;
using Web.ViewModels.Shop;

namespace Web.Services.Abstract
{
    public interface IShopService
    {
        Task<ShopIndexVM> GetAsync(ShopIndexVM model);
        Task<List<Product>> LoadProductsAsync(int id);
        Task<SingleProductIndexVM> GetProductByIdAsync(int id);
    }
}
