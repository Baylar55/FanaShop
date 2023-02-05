using Core.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithBrandAndCategoryAsync();
        Task<Product> GetUpdateModelAsync(int id);
        Task<Product> GetProductDetailsAsync(int id);
        Task<List<Product>> GetBestSellerAsync();
        Task<List<Product>> GetRelatedAsync(int id);
        Task<IQueryable<Product>> PaginateProductsAsync(int page, int take, IQueryable<Product> products);
        Task<int> GetPageCountAsync(int take, IQueryable<Product> products);
        IQueryable<Product> FilterByName(string name);
        IQueryable<Product> FilterByCategory(string name, IQueryable<Product> products);
        IQueryable<Product> FilterByBrand(string name, IQueryable<Product> products);
        IQueryable<Product> FilterBySize(int sizeId, IQueryable<Product> products);
        IQueryable<Product> FilterByColor(int colorId, IQueryable<Product> products);
        Task<IQueryable<Product>> FilterByPrice(IQueryable<Product> products, double? minPrice, double? maxPrice);
        Task<List<Product>> GetAllByCategoryAsync(int id);
    }
}
