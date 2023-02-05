using Core.Entities.Base;

namespace Core.Entities
{
    public class Feature : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconCode { get; set; }
    }
}
