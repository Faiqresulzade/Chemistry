using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.QuizCategory;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class QuizCategoryService : IQuizCategoryService
    {
        private readonly IQuizCategoryRepository _quizCategoryRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ModelStateDictionary _modelstate;

        public QuizCategoryService(IQuizCategoryRepository quizCategoryRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService,
            IWebHostEnvironment webHostEnvironment)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _quizCategoryRepository = quizCategoryRepository;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<QuizCategoryIndexVM> IndexAsync()
        {
            var model = new QuizCategoryIndexVM()
            {
                QuizCategories = await _quizCategoryRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(QuizCategoryCreatVM model)
        {
            if (!_modelstate.IsValid) return false;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            var quizCategory = new QuizCategory()
            {
                Name = model.Name,
                CreateAt = DateTime.Now,
                Photo = model.PhotoPath,
                İsPaid = model.IsPaid
            };

            await _quizCategoryRepository.CreateAsync(quizCategory);
            return true;
        }

        public async Task<QuizCategoryUpdateVM> UpdateAsync(int id)
        {
            var quizCategory=await _quizCategoryRepository.GetAsync(id);
            if (quizCategory == null) return null;

            var model = new QuizCategoryUpdateVM()
            {
                ModefiedAt = DateTime.Now,
                Name = quizCategory.Name,
                PhotoPath = quizCategory.Photo,
                IsPaid = quizCategory.İsPaid

            };
            return model;
        }

        public async Task<bool> UpdateAsync(int id, QuizCategoryUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;

            var quizCategory = await _quizCategoryRepository.GetAsync(id);
            if(quizCategory == null) return false;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 260))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 260kbdan boyukdur!!");
                    return false;
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, quizCategory.Photo);
                quizCategory.Photo = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

            quizCategory.ModifiedAt = DateTime.Now;
            quizCategory.Name = model.Name;
            quizCategory.İsPaid = model.IsPaid;
            await _quizCategoryRepository.SaveChanges();
            return true;
        }

        public async Task<QuizCategory> GetDeleteAsync(int id)
        {
            var quizCategory=await _quizCategoryRepository.GetAsync(id);
            return quizCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var quizCategory = await _quizCategoryRepository.GetAsync(id);
            if(quizCategory == null) return false;

            _fileService.Delete(_webHostEnvironment.WebRootPath, quizCategory.Photo);
            await _quizCategoryRepository.DeleteAsync(quizCategory);
            return true;
        }
    }
}
