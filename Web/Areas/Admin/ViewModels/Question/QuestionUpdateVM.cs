using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Question
{
    public class QuestionUpdateVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Answer { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int FAQCategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}
