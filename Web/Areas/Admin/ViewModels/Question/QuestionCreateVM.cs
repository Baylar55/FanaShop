using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Question
{
    public class QuestionCreateVM
    {
        [Required]
        public string Title { get; set; }

        [MinLength(10)]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int FAQCategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}
