using Core.Entities.Base;

namespace Core.Entities
{
    public class HomeMainSlider : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoName { get; set; }
        public int Order { get; set; }
    }
}
