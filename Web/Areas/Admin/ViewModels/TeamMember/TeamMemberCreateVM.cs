namespace Web.Areas.Admin.ViewModels.TeamMember
{
    public class TeamMemberCreateVM
    {
        public string Name { get; set; }
        public string Speciality { get; set; }
        public IFormFile Photo { get; set; }
    }
}
