using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels.Wishlist
{
    public class WishlistItemVM
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string PhotoName { get; set; }
    }
}
