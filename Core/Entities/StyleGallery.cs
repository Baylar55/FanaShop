using Core.Entities.Base;

namespace Core.Entities
{
    public class StyleGallery : BaseEntity
    {
        public string PhotoName { get; set; }
        public string CoverLetter { get; set; }
        public int Order { get; set; }
    }
}
