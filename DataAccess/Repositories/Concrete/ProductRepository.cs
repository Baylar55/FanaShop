using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductDetailsAsync(int id)
        {
            return await _context.Product
                                        .Include(p => p.Brand)
                                        .Include(p => p.Category)
                                        .Include(p => p.Colors)
                                        .ThenInclude(p => p.Color)
                                        .Include(p => p.Sizes)
                                        .ThenInclude(p => p.Size)
                                        .Include(p => p.ProductPhotos)
                                        .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<Product> GetUpdateModelAsync(int id)
        {
            return await _context.Product
                                        .Include(p => p.Brand)
                                        .Include(p => p.Colors)
                                        .Include(p => p.Sizes)
                                        .Include(p => p.ProductPhotos)
                                        .FirstOrDefaultAsync(pr => pr.Id == id);
        }

        public async Task<List<Product>> GetBestSellerAsync()
        {
            return await _context.Product
                                         .Where(p => p.BestSeller == true)
                                         .OrderByDescending(p => p.CreatedAt)
                                         .Take(8)
                                         .ToListAsync();
        }

        public async Task<List<Product>> GetRelatedAsync(int id)
        {
            return await _context.Product
                                         .Where(p => p.CategoryId == id)
                                         .OrderByDescending(p => p.CreatedAt)
                                         .ToListAsync();
        }

        public async Task<List<Product>> GetAllWithBrandAndCategoryAsync()
        {
            return await _context.Product
                                        .Include(p => p.Brand)
                                        .Include(p => p.Category)
                                        .ToListAsync();

        }

        #region Filtering

        public async Task<IQueryable<Product>> PaginateProductsAsync(int page, int take, IQueryable<Product> products)
        {
            return products.OrderByDescending(p => p.Id)
                           .Skip((page - 1) * take)
                           .Take(take);
        }

        public async Task<int> GetPageCountAsync(int take, IQueryable<Product> products)
        {
            var pageCount = products.Count();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }

        public IQueryable<Product> FilterByName(string name)
        {
            return _context.Product.Include(p => p.Category).Where(p => !string.IsNullOrEmpty(name) ? p.Name.Contains(name) : true);
        }

        public IQueryable<Product> FilterByCategory(string name, IQueryable<Product> products)
        {
            return products.Where(p => !string.IsNullOrEmpty(name) ? p.Category.Title == name : true);
        }

        public IQueryable<Product> FilterByBrand(string name, IQueryable<Product> products)
        {
            return products.Where(p => !string.IsNullOrEmpty(name) ? p.Brand.Title == name : true);
        }

        public IQueryable<Product> FilterBySize(int sizeId, IQueryable<Product> products)
        {
            if (sizeId != 0)
            {
                return products.Where(p => p.Sizes.Any(s => sizeId == s.SizeId));
            }
            return products;
        }

        public IQueryable<Product> FilterByColor(int colorId, IQueryable<Product> products)
        {
            if (colorId != 0)
            {
                return products.Where(p => p.Colors.Any(c => colorId == c.ColorId));
            }
            return products;
        }

        public async Task<IQueryable<Product>> FilterByPrice(IQueryable<Product> products, double? minPrice, double? maxPrice)
        {
            return products.Where(p => (minPrice != null ? p.Price >= minPrice : true) && (maxPrice != null ? p.Price <= maxPrice : true));
        }

        #endregion

        public async Task<List<Product>> GetAllByCategoryAsync(int id)
        {
            return await _context.Product.Where(p => p.CategoryId == id).ToListAsync();
        }
    }
}
