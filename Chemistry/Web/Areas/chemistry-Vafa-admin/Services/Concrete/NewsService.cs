using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.News;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class NewsService : INewsService
    {
        #region Configuration
        private readonly ModelStateDictionary _modelstate;
        private readonly INewsRepository _newsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        public NewsService(
            INewsRepository newsRepository,
            IActionContextAccessor actionContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext,
            IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _newsRepository = newsRepository;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _fileService = fileService;
        }
        #endregion
        #region Index
        public async Task<NewsIndexVM> Index()
        {
            var model = new NewsIndexVM
            {
                News = await _newsRepository.GetAllAsync()
            };
            return model;
        }
        #endregion
        #region Create
        public async Task<bool> CreateAsync(NewsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                //if (!_fileService.CheckSize(model.Photo, 200))
                //{
                //    _modelstate.AddModelError("Photo", "Şəkilin ölçüsü 200KBdan böyükdür!!");
                //    return false;
                //}
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            var news = new News
            {
                Category = model.Category,
                CreateAt = DateTime.Now,
                Info = model.Info,
                Title = model.Title,
                Photo = model.PhotoPath
            };
            await _newsRepository.CreateAsync(news);
            return true;
        }
        #endregion
        #region Update

        public async Task<NewsUpdateVM> UpdateAsync(int id)
        {
            var news=await _newsRepository.GetAsync(id);
            if (news == null)
            {
                _modelstate.AddModelError("Title", "Bele news movcud deyil!!");
                return null;
            }

            var model = new NewsUpdateVM
            {
                Info=news.Info,
                Category=news.Category,
                Title=news.Title,
                PhotoPath=news.Photo
            };
            return model;
        }
        public async Task<bool> UpdateAsync(NewsUpdateVM model, int id)
        {

            if (!_modelstate.IsValid) return false;
            var news = await _newsRepository.GetAsync(id);
            if (news == null) return false;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 200))
                {
                    _modelstate.AddModelError("Photo", "Şəkilin ölçüsü 200KBdan böyükdür!!");
                    return false;
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath,news.Photo);
                news.Photo = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }
            news.Title = model.Title;
            news.Info = model.Info;
            news.ModifiedAt = DateTime.Now;
            news.Category = model.Category;
            await _newsRepository.SaveChanges();
            return true;
        }
        #endregion
        #region Delete
        public async Task<News> GetDelete(int id)
        {
            var news=await _newsRepository.GetAsync(id);
            return news;
        }

        public async Task<bool> Delete(int id)
        {
            var news = await _newsRepository.GetAsync(id);
            if (news == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, news.Photo);
            await _newsRepository.DeleteAsync(news);
            return true;
        }
        #endregion
        #region Details
        public async Task<News> DetailsAsync(int id)
        {
            var news = await _newsRepository.GetAsync(id);
            return news;
        }
        #endregion
    }
}
