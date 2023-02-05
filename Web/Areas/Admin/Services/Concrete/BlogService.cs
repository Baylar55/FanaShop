using Core.Entities;
using Core.Utilities.Helpers;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Blog;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogPhotoRepository _blogPhotoRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public BlogService(IBlogRepository blogRepository,
                           IBlogPhotoRepository blogPhotoRepository,
                           IActionContextAccessor actionContextAccessor,
                           ICategoryRepository categoryRepository,
                           IFileService fileService)

        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _blogPhotoRepository = blogPhotoRepository;
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
            _fileService = fileService;
        }

        #region Blog Crud

        public async Task<BlogIndexVM> GetAllAsync()
        {
            var model = new BlogIndexVM()
            {
                Blogs = await _blogRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<BlogCreateVM> GetCreateModelAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            var model = new BlogCreateVM
            {
                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<BlogUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _categoryRepository.GetAllAsync();
            var blog = await _blogRepository.GetWithPhotosAsync(id);
            if (blog == null) return null;
            var model = new BlogUpdateVM()
            {
                Title = blog.Title,
                Author = blog.Author,
                Description = blog.Description,
                Quote = blog.Quote,
                QuoteAuthor = blog.QuoteAuthor,
                CategoryId = blog.CategoryId,
                BlogPhotos = blog.BlogPhotos,

                Categories = category.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<BlogDetailsVM> DetailsAsync(int id)
        {
            var Blog = await _blogRepository.GetWithPhotosAsync(id);
            if (Blog == null) return null;
            var model = new BlogDetailsVM()
            {
                Title = Blog.Title,
                Author = Blog.Author,
                Quote = Blog.Quote,
                Category = Blog.Category.Title,
                QuoteAuthor = Blog.QuoteAuthor,
                Description = Blog.Description,
                CoverPhotoName = Blog.CoverPhoto,
                BlogPhotos = Blog.BlogPhotos,
                CreatedAt = Blog.CreatedAt,
                ModifiedAt = Blog.ModifiedAt,
            };
            return model;
        }

        public async Task<bool> CreateAsync(BlogCreateVM model)
        {
            var category = await _categoryRepository.GetAllAsync();
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
            if (await _categoryRepository.GetAsync(model.CategoryId) == null)
            {
                _modelState.AddModelError("CategoryId", "This category isn't exist");
                return false;
            }
            if (!_modelState.IsValid) return false;
            bool isExist = await _blogRepository.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                _modelState.AddModelError("Name", "This name is already exist");
                return false;
            }
            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("Photo", "Photo should be in image format");
                return false;
            }
            else if (!_fileService.CheckSize(model.CoverPhoto, 400))
            {
                _modelState.AddModelError("Photo", $"Photo's size should be smaller than 400kb");
                return false;
            }
            var blog = new Blog
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                CategoryId = model.CategoryId,
                QuoteAuthor = model.QuoteAuthor,
                Quote = model.Quote,
                CreatedAt = DateTime.Now,
                CoverPhoto = await _fileService.UploadAsync(model.CoverPhoto)
            };
            await _blogRepository.CreateAsync(blog);

            bool hasError = false;
            foreach (var photo in model.Photos)
            {
                if (!_fileService.IsImage(photo))
                {
                    _modelState.AddModelError("Photo", $"{photo.FileName} should be in image format");
                    hasError = true;
                }
                else if (!_fileService.CheckSize(photo, 400))
                {
                    _modelState.AddModelError("Photo", $"{photo.FileName}'s size sould be smaller than 400kb");
                    hasError = true;
                }
            }
            if (hasError) return hasError;

            int order = 1;
            foreach (var photo in model.Photos)
            {
                var BlogPhoto = new BlogPhoto
                {
                    BlogId = blog.Id,
                    PhotoName = await _fileService.UploadAsync(photo),
                    Order = order++,
                    CreatedAt = DateTime.Now,
                };
                await _blogPhotoRepository.CreateAsync(BlogPhoto);
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);
            if (blog == null) return false;
            _fileService.Delete(blog.CoverPhoto);
            foreach (var item in blog.BlogPhotos)
            {
                _fileService.Delete(item.PhotoName);
            }
            await _blogRepository.DeleteAsync(blog);
            return true;
        }

        public async Task<bool> UpdateAsync(BlogUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            if (model == null) return false;
            var category = await _categoryRepository.GetAllAsync();
            model.Categories = category.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
            var Blog = await _blogRepository.GetAsync(model.Id);
            if (Blog == null) return false;
            bool isExist = await _blogRepository.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim() && p.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "this title is already exist");
                return false;
            }

            Blog.Title = model.Title;
            Blog.Author = model.Author;
            Blog.Quote = model.Quote;
            Blog.QuoteAuthor = model.QuoteAuthor;
            Blog.CategoryId = model.CategoryId;
            Blog.Description = model.Description;
            Blog.ModifiedAt = DateTime.Now;
            bool hasError = false;
            if (model.Photos != null)
            {
                foreach (var photo in model.Photos)
                {
                    if (!_fileService.IsImage(photo))
                    {
                        _modelState.AddModelError("Photos", $"{photo.Name} must be image");
                        hasError = true;
                    }
                    else if (!_fileService.CheckSize(photo, 400))
                    {
                        _modelState.AddModelError("Photos", $"{photo.Name} size must be less than 400kb");
                        hasError = true;
                    }

                    int order = 1;
                    var BlogPhoto = new BlogPhoto
                    {
                        PhotoName = await _fileService.UploadAsync(photo),
                        Order = order,
                        BlogId = Blog.Id
                    };
                    order++;
                    await _blogPhotoRepository.UpdateAsync(BlogPhoto);
                }
            }
            await _blogRepository.UpdateAsync(Blog);
            return true;
        }

        #endregion

        #region BlogPhoto Crud

        public async Task<BlogPhotoUpdateVM> GetBlogPhotoUpdateAsync(int id)
        {
            var blogPhoto = await _blogPhotoRepository.GetAsync(id);
            if (blogPhoto == null) return null;
            var model = new BlogPhotoUpdateVM
            {
                Id = blogPhoto.Id,
                Order = blogPhoto.Order,
            };
            return model;
        }

        public async Task<bool> UpdatePhotoAsync(int id, BlogPhotoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var blogPhoto = await _blogPhotoRepository.GetAsync(id);
            if (blogPhoto == null) return false;
            blogPhoto.Order = model.Order;
            model.BlogId = blogPhoto.BlogId;
            await _blogPhotoRepository.UpdateAsync(blogPhoto);
            return true;
        }

        public async Task<bool> DeletePhotoAsync(int id, BlogPhotoDeleteVM model)
        {
            var BlogPhoto = await _blogPhotoRepository.GetAsync(id);
            if (BlogPhoto == null) return false;
            _fileService.Delete(BlogPhoto.PhotoName);
            model.BlogId = BlogPhoto.BlogId;
            await _blogPhotoRepository.DeleteAsync(BlogPhoto);
            return true;
        }

        #endregion
    }
}
