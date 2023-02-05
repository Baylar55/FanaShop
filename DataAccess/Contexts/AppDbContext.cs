using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HomeMainSlider> HomeMainSlider { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FAQCategory> FAQCategory { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Characteristic> Characteristic { get; set; }
        public DbSet<AboutIntro> AboutIntro { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<PriceRange> PriceRange { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogPhoto> BlogPhoto { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<ProductSize> ProductSize { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketProduct> BasketProduct { get; set; }
        public DbSet<ContactComponent> ContactComponent { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<StyleGallery> StyleGallery { get; set; }
    }
}
