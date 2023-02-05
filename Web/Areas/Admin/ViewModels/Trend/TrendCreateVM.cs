namespace Web.Areas.Admin.ViewModels.Trend
{
    public class TrendCreateVM
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
