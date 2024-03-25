using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.VideoLesson;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class VideoLessonService : IVideoLessonService
    {
        #region configuration
        private readonly ModelStateDictionary _modelstate;
        private readonly IVideoLessonRepository _videoLessonRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly AppDbContext _appDbContext;

        public VideoLessonService(IVideoLessonRepository videoLessonRepository,
            IActionContextAccessor actionContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            AppDbContext appDbContext)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _videoLessonRepository = videoLessonRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _appDbContext = appDbContext;
        }
        #endregion
        #region Index
        public async Task<VideoLessonIndexVM> IndexAsync()
        {
            var model = new VideoLessonIndexVM()
            {
                VideoLesson = await _appDbContext.VideoLessons.Include(c=>c.Category).ToListAsync(),
            };
            return model;
        }
        #endregion
        #region create


        public async Task<VideoLessonCreateVM> CreateAsync()
        {
            var model = new VideoLessonCreateVM()
            {
                Categories = await _appDbContext.VideoLessonCategories.Select(c => new SelectListItem
                {
                    Text = c.CategoryTitle,
                    Value = c.Id.ToString()
                })
                .ToListAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(VideoLessonCreateVM model)
        {
            model.Categories = await _appDbContext.VideoLessonCategories.Select(c => new SelectListItem
            {
                Text = c.CategoryTitle,
                Value = c.Id.ToString()
            })
                .ToListAsync();

            if (!_modelstate.IsValid) return false;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                //if (!_fileService.CheckSize(model.Photo, 160))
                //{
                //    _modelstate.AddModelError("Photo", "sekiln olcusu 60kbdan boyukdur!!");
                //    return false;
                //}
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            var video = new VideoLesson()
            {
                CreateAt = DateTime.Now,
                Photo = model.PhotoPath,
                Title = model.Title,
                Video = model.VideoPath,
                CategoryID=model.CategoryId,
                İsPaid=model.İsPaid
            };
            await _videoLessonRepository.CreateAsync(video);
            return true;
        }


        #endregion
        #region update
        public async Task<VideoLessonUpdateVM> UpdateAsync(int id)
        {
            var videoLesson=await _videoLessonRepository.GetAsync(id);
            if (videoLesson == null) return null;
            var model = new VideoLessonUpdateVM()
            {
                Title = videoLesson.Title,
                PhotoPath=videoLesson.Photo,
                VideoPath=videoLesson.Video,
                CategoryId=videoLesson.CategoryID,
                Categories = await _appDbContext.VideoLessonCategories.Select(c => new SelectListItem
                {
                    Text = c.CategoryTitle,
                    Value = c.Id.ToString()
                })
                .ToListAsync()
            };
            return model;
        }

        public async Task<bool> UpdateAsync(int id, VideoLessonUpdateVM model)
        {
            model.Categories =await _appDbContext.VideoLessonCategories.Select(c => new SelectListItem
            {
                Text = c.CategoryTitle,
                Value = c.Id.ToString()
            }).ToListAsync();

            if (!_modelstate.IsValid) return false;
            var videoLesson = await _videoLessonRepository.GetAsync(id);
            if (videoLesson == null) return false;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                //if (!_fileService.CheckSize(model.Photo, 160))
                //{
                //    _modelstate.AddModelError("Photo", "sekiln olcusu 160kbdan boyukdur!!");
                //    return false;
                //}
                _fileService.Delete(_webHostEnvironment.WebRootPath, videoLesson.Photo);
                videoLesson.Photo = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            var category = await _appDbContext.VideoLessonCategories.FindAsync(model.CategoryId);
            if (category == null) return false;

            videoLesson.Title = model.Title;
            videoLesson.Video = model.VideoPath;
            videoLesson.CategoryID = category.Id;
            videoLesson.ModifiedAt = DateTime.Now;
            videoLesson.İsPaid = model.İspaid;
            await _videoLessonRepository.SaveChanges();
            return true;
        }
        #endregion
        #region Delete

        public async Task<VideoLesson> GetDeleteAsync(int id)
        {
            var videoLesson = await _videoLessonRepository.GetAsync(id);
            return videoLesson;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var videoLesson = await _videoLessonRepository.GetAsync(id);
            if (videoLesson == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, videoLesson.Photo);
            await _videoLessonRepository.DeleteAsync(videoLesson);
            return true;
        }
        #endregion
    }
}
