using Core.Entities.Base;

namespace Core.Entities
{
    public class TeamMember : BaseEntity
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string PhotoName { get; set; }
    }
}
