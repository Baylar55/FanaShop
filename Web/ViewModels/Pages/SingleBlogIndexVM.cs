using Core.Entities;

namespace Web.ViewModels.Pages
{
    public class SingleBlogIndexVM
    {
        public Blog Blog { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
