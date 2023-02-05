using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPhotoRepository _productPhotoRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ModelStateDictionary _modelState;

        public ProductService(IProductRepository productRepository,
                              IProductPhotoRepository productPhotoRepository,
                              IBrandRepository brandRepository,
                              IActionContextAccessor actionContextAccessor,
                              IFileService fileService,
                              IColorRepository colorRepository,
                              ISizeRepository sizeRepository,
                              IProductColorRepository productColorRepository,
                              IProductSizeRepository productSizeRepository,
                              ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _productPhotoRepository = productPhotoRepository;
            _brandRepository = brandRepository;
            _fileService = fileService;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _productColorRepository = productColorRepository;
            _productSizeRepository = productSizeRepository;
            _categoryRepository = categoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<List<SelectListItem>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            return brands.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task<ProductCreateVM> GetBrandCreateModelAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();
            var colors = await _colorRepository.GetAllAsync();
            var model = new ProductCreateVM
            {
                Brands = brands.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList(),

                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList(),
            };
            return model;
        }

        public async Task<bool> CreateAsync(ProductCreateVM model)
        {
            model.Brands = await GetAllBrandsAsync();
            model.Categories = await GetAllCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This product already created");
                return false;
            }
            if (!_fileService.IsImage(model.MainPhoto))
            {
                _modelState.AddModelError("MainPhoto", $"File must be Image");
                return false;
            }

            else if (!_fileService.CheckSize(model.MainPhoto, 500))
            {
                _modelState.AddModelError("MainPhoto", $"Photo size must be less than 500kb ");
                return false;
            }
            if (!_fileService.IsImage(model.BackPhoto))
            {
                _modelState.AddModelError("BackPhoto", $"File must be Image");
                return false;
            }

            else if (!_fileService.CheckSize(model.BackPhoto, 500))
            {
                _modelState.AddModelError("BackPhoto", $"Photo size must be less than 500kb ");
                return false;
            }
            var product = new Product
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
                BestSeller = model.BestSeller,
                Description = model.Description,
                Price = model.Price,
                Status = model.Status,
                MainPhoto = await _fileService.UploadAsync(model.MainPhoto),
                BackPhoto = await _fileService.UploadAsync(model.BackPhoto),
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
            };

            await _productRepository.CreateAsync(product);

            bool hasError = false;

            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("Photos", $"File must be Image");
                }
                else if (!_fileService.CheckSize(photo, 600))
                {
                    _modelState.AddModelError("Photos", $"Photo size must be less than 600kb ");
                }
            }
            if (hasError)
            {
                return false;
            }

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var productPhoto = new ProductPhoto
                {
                    Name = await _fileService.UploadAsync(photo),
                    Order = order,
                    ProductId = product.Id,
                    CreatedAt = DateTime.Now,
                };
                order++;
                await _productPhotoRepository.CreateAsync(productPhoto);
            }
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<ProductIndexVM> GetAllAsync()
        {
            var model = new ProductIndexVM
            {
                Products = await _productRepository.GetAllWithBrandAndCategoryAsync(),
            };
            return model;
        }

        public async Task<ProductUpdateVM> GetUpdateModelAsync(int id)
        {
            var product = await _productRepository.GetUpdateModelAsync(id);
            var model = new ProductUpdateVM
            {
                Id = product.Id,
                BestSeller = product.BestSeller,
                Description = product.Description,
                Price = product.Price,
                Status = product.Status,
                Name = product.Name,
                Brands = await GetAllBrandsAsync(),
                BrandId = product.BrandId,
                Categories = await GetAllCategoriesAsync(),
                CategoryId = product.CategoryId,
                Colors = await GetColorsSelectAsync(),
                Sizes = await GetSizesSelectAsync(),
                CurrentColors = await _productColorRepository.GetProductColorsAsync(product.Id),
                CurrentSizes = await _productSizeRepository.GetProductSizesAsync(product.Id),
                ProductPhotos = product.ProductPhotos
            };

            return model;
        }

        public async Task<bool> UpdateAsync(ProductUpdateVM model)
        {
            model.Brands = await GetAllBrandsAsync();
            model.Categories = await GetAllCategoriesAsync();
            if (!_modelState.IsValid) return false;
            var isExist = await _productRepository.AnyAsync(pr => pr.Name.Trim().ToLower() == model.Name.Trim().ToLower()
            && model.Id != pr.Id);
            if (isExist)
            {
                _modelState.AddModelError("Name", "This product already created");
                return false;
            }
            var product = await _productRepository.GetProductDetailsAsync(model.Id);
            product.Name = model.Name;
            product.BestSeller = model.BestSeller;
            product.ModifiedAt = DateTime.Now;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Status = model.Status;
            product.BrandId = model.BrandId;
            product.CategoryId = model.CategoryId;
            if (model.MainPhoto != null)
            {
                if (!_fileService.IsImage(model.MainPhoto))
                {
                    _modelState.AddModelError("MainPhoto", $"File must be Image");
                    return false;
                }
                else if (!_fileService.CheckSize(model.MainPhoto, 600))
                {
                    _modelState.AddModelError("MainPhoto", $"Photo size must be less than 600kb");
                    return false;
                }
                _fileService.Delete(product.MainPhoto);
                product.MainPhoto = await _fileService.UploadAsync(model.MainPhoto);
            }

            if (model.BackPhoto != null)
            {
                if (!_fileService.IsImage(model.BackPhoto))
                {
                    _modelState.AddModelError("BackPhoto", $"File must be Image");
                    return false;
                }
                else if (!_fileService.CheckSize(model.BackPhoto, 600))
                {
                    _modelState.AddModelError("BackPhoto", $"Photo size must be less than 600kb");
                    return false;
                }
                _fileService.Delete(product.BackPhoto);
                product.BackPhoto = await _fileService.UploadAsync(model.BackPhoto);
            }

            if (model.Photos != null)
            {
                bool hasError = false;
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.IsImage(photo))
                    {
                        _modelState.AddModelError("Photos", $"File must be Image");
                    }
                    else if (!_fileService.CheckSize(photo, 600))
                    {
                        _modelState.AddModelError("Photos", $"Photo size must be less than 600kb");
                    }
                }
                if (hasError)
                {
                    return false;
                }

                int order = 1;
                foreach (var photo in model.Photos)
                {
                    var productPhoto = new ProductPhoto
                    {
                        Name = await _fileService.UploadAsync(photo),
                        Order = order,
                        ProductId = product.Id,
                        CreatedAt = DateTime.Now,
                    };
                    order++;
                    await _productPhotoRepository.CreateAsync(productPhoto);
                }
            }

            if (model.ColorsIds != null)
            {
                foreach (var color in product.Colors)
                {
                    await _productColorRepository.DeleteAsync(color);
                }
                foreach (var colorId in model.ColorsIds)
                {
                    var color = await _colorRepository.GetAsync(colorId);
                    if (color == null) return false;
                    var productColor = new ProductColor
                    {
                        ColorId = color.Id,
                        ModifiedAt = DateTime.Now,
                        ProductId = product.Id,
                    };
                    await _productColorRepository.CreateAsync(productColor);
                }
            }

            if (model.SizesIds != null)
            {
                foreach (var size in product.Sizes)
                {
                    await _productSizeRepository.DeleteAsync(size);
                }
                foreach (var sizeId in model.SizesIds)
                {
                    var size = await _sizeRepository.GetAsync(sizeId);
                    if (size == null) return false;
                    var productSize = new ProductSize
                    {
                        ProductId = product.Id,
                        ModifiedAt = DateTime.Now,
                        SizeId = size.Id,
                    };
                    await _productSizeRepository.CreateAsync(productSize);
                }
            }
            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> AddColorAsync(ProductAddColorVM model)
        {
            model.Colors = await GetColorsSelectAsync();
            if (!_modelState.IsValid) return false;
            var product = await _productRepository.GetAsync(model.ProductId);
            if (product == null) return false;
            var hasError = false;
            foreach (var colorId in model.ColorsIds)
            {
                var color = await _colorRepository.GetAsync(colorId);
                if (color == null) return false;
                var isExist = await _productColorRepository.AnyAsync(c => c.ProductId == product.Id && c.ColorId == colorId);
                if (isExist)
                {
                    _modelState.AddModelError(string.Empty, $"{color.Name} color already added");
                    hasError = true;
                }
            }
            if (hasError)
            {
                return false;
            }
            else
            {
                foreach (var colorId in model.ColorsIds)
                {
                    var color = await _colorRepository.GetAsync(colorId);
                    var productColor = new ProductColor
                    {
                        ProductId = product.Id,
                        ColorId = color.Id,
                        CreatedAt = DateTime.Now,
                    };
                    await _productColorRepository.CreateAsync(productColor);
                }
            }

            return true;
        }

        public async Task<bool> AddSizeAsync(ProductAddSizeVM model)
        {
            model.Sizes = await GetSizesSelectAsync();
            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetAsync(model.ProductId);
            if (product == null) return false;
            var hasError = false;
            foreach (var sizeId in model.SizesIds)
            {
                var size = await _sizeRepository.GetAsync(sizeId);
                if (size == null) return false;
                var isExist = await _productSizeRepository.AnyAsync(s => s.ProductId == product.Id && s.SizeId == sizeId);
                if (isExist)
                {
                    _modelState.AddModelError(string.Empty, $"{size.Title} size already added");
                    hasError = true;
                }
            }
            if (hasError)
            {
                return false;
            }
            else
            {
                foreach (var sizeId in model.SizesIds)
                {
                    var size = await _sizeRepository.GetAsync(sizeId);
                    var productSize = new ProductSize
                    {
                        ProductId = product.Id,
                        SizeId = size.Id,
                        CreatedAt = DateTime.Now,
                    };
                    await _productSizeRepository.CreateAsync(productSize);
                }
            }
            return true;
        }

        public async Task<ProductAddColorVM> GetColorAddModelAsync()
        {
            var colors = await _colorRepository.GetAllAsync();
            var model = new ProductAddColorVM
            {
                Colors = colors.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }).ToList(),

            };
            return model;
        }

        public async Task<ProductAddSizeVM> GetSizeAddModelAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            var model = new ProductAddSizeVM
            {
                Sizes = sizes.Select(s => new SelectListItem
                {
                    Text = s.Title,
                    Value = s.Id.ToString(),
                }).ToList()
            };
            return model;
        }

        public async Task<List<SelectListItem>> GetColorsSelectAsync()
        {
            var colors = await _colorRepository.GetAllAsync();
            return colors.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetSizesSelectAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return sizes.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Id.ToString()
            }).ToList();
        }

        public async Task<ProductDetailsVM> GetProductDetailsAsync(int id)
        {
            var model = new ProductDetailsVM
            {
                Product = await _productRepository.GetProductDetailsAsync(id),
            };
            return model;
        }

        public async Task<bool> PhotoUpdateAsync(ProductPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var photo = await _productPhotoRepository.GetAsync(model.Id);
            if (photo == null) return false;

            photo.Order = model.Order;
            await _productPhotoRepository.UpdateAsync(photo);
            return true;
        }

        public async Task<ProductPhotoUpdateVM> GetPhotoUpdateModelAsync(int id)
        {
            var photo = await _productPhotoRepository.GetAsync(id);
            if (photo == null) return null;
            var model = new ProductPhotoUpdateVM
            {
                Id = photo.Id,
                Order = photo.Order,
                ProductId = photo.ProductId,
            };
            return model;
        }

        public async Task DeletePhotoAsync(int id)
        {
            var productPhoto = await _productPhotoRepository.GetAsync(id);
            await _productPhotoRepository.DeleteAsync(productPhoto);
        }
    }
}
