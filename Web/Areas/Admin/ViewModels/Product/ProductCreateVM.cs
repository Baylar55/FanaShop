using Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
        public ProductStatus? Status { get; set; }
        public IFormFile MainPhoto { get; set; }
        public IFormFile BackPhoto { get; set; }
        public List<IFormFile> Photos { get; set; }
        public bool BestSeller { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public List<SelectListItem>? Brands { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
