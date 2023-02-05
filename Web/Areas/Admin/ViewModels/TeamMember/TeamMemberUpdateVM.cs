namespace Web.Areas.Admin.ViewModels.TeamMember
{
    public class TeamMemberUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Speciality { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
