using Web.ViewModels.Wishlist;

namespace Web.Services.Abstract
{
    public interface IWishlistService
    {
        Task<bool> WishlistAddAsync(WishlistAddVM model);
        Task<List<WishlistItemVM>> GetAllAsync();
        Task<bool> RemoveAsync(int id);
    }
}
