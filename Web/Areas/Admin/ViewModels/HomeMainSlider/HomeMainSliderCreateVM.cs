﻿namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
    }
}
