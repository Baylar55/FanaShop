namespace Web.Areas.Admin.ViewModels.Trend
{
    public class TrendUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
