using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Quiz;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class QuizService : IQuizService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IQuizzesRepository _quizzesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;

        public QuizService(IQuizzesRepository quizzesRepository,IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext,
              IActionContextAccessor actionContextAccessor,IFileService fileService)
        {
            _quizzesRepository = quizzesRepository;
           _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _fileService = fileService;
            _modelstate = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<QuizIndexVM> IndexAsync()
        {
            var model = new QuizIndexVM()
            {
                Quizzes = await _appDbContext.Quizzes.Include(c => c.QuizCategory).ToListAsync()
            };
            return model;
        }


        public async Task<QuizCreateVM> CreateAsync()
        {
            var model = new QuizCreateVM()
            {
                Categories = await _appDbContext.QuizCategories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToListAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(QuizCreateVM model)
        {
            model.Categories = await _appDbContext.QuizCategories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();

           

            if (!_modelstate.IsValid) return false;

            if (model.QuizImage != null)
            {
                if (!_fileService.IsImage(model.QuizImage))
                {
                    _modelstate.AddModelError("QuizImage", "Yüklədiyiniz fayl şəkil formatında deyil!");
                    return false;
                }
                model.ImagePath = await _fileService.Upload(model.QuizImage, _webHostEnvironment.WebRootPath);
            }

            if (model.CorrectVariant != "A" ^ model.CorrectVariant != "B" ^ model.CorrectVariant != "C" ^
                model.CorrectVariant != "D" ^ model.CorrectVariant != "E")
            {
                _modelstate.AddModelError(model.CorrectVariant, "Duzgun daxil edin");
                return false;
            }

            var quiz = new Quiz()
            {
                QuizTitle = model.QuizTitle,
                CreateAt = DateTime.Now,
                VariantA = model.VariantA,
                VariantB = model.VariantB,
                VariantC = model.VariantC,
                VariantD = model.VariantD,
                VariantE = model.VariantE,
                QuizCategoryId = model.QuizCategoryId,
                CorrectVariant = model.CorrectVariant,
                QuizImage=model.ImagePath,
            };
            await _quizzesRepository.CreateAsync(quiz);
            return true;

        }


        public async Task<QuizUpdateVM> Update(int id)
        {
            var quiz = await _quizzesRepository.GetAsync(id);
            if (quiz == null) return null;

            QuizUpdateVM model = new QuizUpdateVM()
            {
                CorrectVariant = quiz.CorrectVariant,
                VariantA = quiz.VariantA,
                QuizTitle = quiz.QuizTitle,
                VariantB = quiz.VariantB,
                VariantC = quiz.VariantC,
                VariantD = quiz.VariantD,
                VariantE = quiz.VariantE,
                QuizCategoryId = quiz.QuizCategoryId,
                Categories = await _appDbContext.QuizCategories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync(),
            };
            return model;
        }

        public async Task<bool> Update(int id, QuizUpdateVM model)
        {
            model.Categories = await _appDbContext.QuizCategories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToListAsync();

            if (!_modelstate.IsValid) return false;

            var quiz = await _quizzesRepository.GetAsync(id);

            if (quiz == null) return false;

            if (model.QuizImage != null)
            {
                if (!_fileService.IsImage(model.QuizImage))
                {
                    _modelstate.AddModelError("QuizImage", "Yüklədiyiniz fayl şəkil formatında deyil!");
                    return false;
                }
                model.ImagePath = await _fileService.Upload(model.QuizImage, _webHostEnvironment.WebRootPath);
            }
            var category = await _appDbContext.QuizCategories.FindAsync(id);
            if (category == null) return false;

            quiz.CorrectVariant = model.CorrectVariant;
            quiz.VariantA = model.VariantA;
            quiz.VariantB = model.VariantB;
            quiz.VariantC = model.VariantC;
            quiz.VariantD = model.VariantD;
            quiz.VariantE = model.VariantE;
            quiz.ModifiedAt = DateTime.Now;
            quiz.QuizTitle = model.QuizTitle;
            quiz.QuizCategoryId = model.QuizCategoryId;
            quiz.QuizImage = model.ImagePath;

            await _quizzesRepository.SaveChanges();

            return true;
        }

        public async Task<Quiz> GetdeleteAsync(int id)
        {
            var quiz = await _quizzesRepository.GetAsync(id);
            return quiz;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var quiz = await _quizzesRepository.GetAsync(id);
            if (quiz == null) return false;
            await _quizzesRepository.DeleteAsync(quiz);
            return true;
        }

        public async Task<Quiz> Details(int id)
        {
            var quiz = await _appDbContext.Quizzes.Include(c => c.QuizCategory).FirstOrDefaultAsync();
            return quiz;
        }
    }
}
