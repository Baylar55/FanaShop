using Core.Entities.Base;

namespace Core.Entities
{
    public class Feedback : BaseEntity
    {
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
    }
}
