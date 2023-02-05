using Core.Entities.Base;

namespace Core.Entities
{
    public class BlogPhoto : BaseEntity
    {
        public string PhotoName { get; set; }
        public int Order { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }

    }
}
