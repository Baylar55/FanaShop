namespace Web.Areas.Admin.ViewModels.PriceRange
{
    public class PriceRangeUpdateVM
    {
        public int Id { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int Step { get; set; }
    }
}
