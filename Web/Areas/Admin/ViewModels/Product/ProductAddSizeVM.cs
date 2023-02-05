using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductAddSizeVM
    {
        public int ProductId { get; set; }
        public List<int> SizesIds { get; set; }
        public List<SelectListItem>? Sizes { get; set; }
    }
}
