using Core.Entities.Base;

namespace Core.Entities
{
    public class AboutIntro : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
    }
}
