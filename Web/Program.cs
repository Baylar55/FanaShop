using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concrete;
using AdminAbstractServices = Web.Areas.Admin.Services.Abstract;
using AdminConcreteServices = Web.Areas.Admin.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IFileService, FileService>();


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 0;
})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories

builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IFAQCategoryRepository, FAQCategoryRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
builder.Services.AddScoped<IAboutIntroRepository, AboutIntroRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IPriceRangeRepository, PriceRangeRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogPhotoRepository, BlogPhotoRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IContactComponentRepository, ContactComponentRepository>();
builder.Services.AddScoped<IStyleGalleryRepository, StyleGalleryRepository>();
builder.Services.AddScoped<ITrendRepository, TrendRepository>();

#endregion


#region Services

builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

builder.Services.AddScoped<AdminAbstractServices.IAccountService, AdminConcreteServices.AccountService>();
builder.Services.AddScoped<AdminAbstractServices.IHomeMainSliderService, AdminConcreteServices.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractServices.IFeatureService, AdminConcreteServices.FeatureService>();
builder.Services.AddScoped<AdminAbstractServices.IFAQCategoryService, AdminConcreteServices.FAQCategoryService>();
builder.Services.AddScoped<AdminAbstractServices.IQuestionService, AdminConcreteServices.QuestionService>();
builder.Services.AddScoped<AdminAbstractServices.ITeamMemberService, AdminConcreteServices.TeamMemberService>();
builder.Services.AddScoped<AdminAbstractServices.IFeedbackService, AdminConcreteServices.FeedbackService>();
builder.Services.AddScoped<AdminAbstractServices.ICharacteristicService, AdminConcreteServices.CharacteristicService>();
builder.Services.AddScoped<AdminAbstractServices.IAboutIntroService, AdminConcreteServices.AboutIntroService>();
builder.Services.AddScoped<AdminAbstractServices.ICategoryService, AdminConcreteServices.CategoryService>();
builder.Services.AddScoped<AdminAbstractServices.IBrandService, AdminConcreteServices.BrandService>();
builder.Services.AddScoped<AdminAbstractServices.IColorService, AdminConcreteServices.ColorService>();
builder.Services.AddScoped<AdminAbstractServices.IPriceRangeService, AdminConcreteServices.PriceRangeService>();
builder.Services.AddScoped<AdminAbstractServices.IBlogService, AdminConcreteServices.BlogService>();
builder.Services.AddScoped<AdminAbstractServices.ISizeService, AdminConcreteServices.SizeService>();
builder.Services.AddScoped<AdminAbstractServices.IProductService, AdminConcreteServices.ProductService>();
builder.Services.AddScoped<AdminAbstractServices.ILocationService, AdminConcreteServices.LocationService>();
builder.Services.AddScoped<AdminAbstractServices.IContactComponentService, AdminConcreteServices.ContactComponentService>();
builder.Services.AddScoped<AdminAbstractServices.IStyleGalleryService, AdminConcreteServices.StyleGalleryService>();
builder.Services.AddScoped<AdminAbstractServices.ITrendService, AdminConcreteServices.TrendService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
}
app.Run();
