using Core.Entities.Base;

namespace Core.Entities
{
    public class Trend : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
    }
}
