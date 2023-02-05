using Core.Entities.Base;

namespace Core.Entities
{
    public class Question : BaseEntity
    {
        public string Title { get; set; }
        public string Answer { get; set; }
        public FAQCategory FAQCategory { get; set; }
        public int FAQCategoryId { get; set; }
    }
}
