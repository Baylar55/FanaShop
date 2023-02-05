using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductAddColorVM
    {
        public int ProductId { get; set; }
        public List<int> ColorsIds { get; set; }
        public List<SelectListItem>? Colors { get; set; }
    }
}
