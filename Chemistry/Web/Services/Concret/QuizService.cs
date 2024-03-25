using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Quiz;

namespace Web.Services.Concret
{
    public class QuizService : IQuizService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IQuizCategoryRepository _quizCategoryRepository;
        private readonly IQuizzesRepository _quizzesRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IQuizAnswersRepository _quizAnswersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        private List<Quiz> quizzes;

        public QuizService(IQuizCategoryRepository quizCategoryRepository,
            IQuizzesRepository quizzesRepository, AppDbContext appDbContext,
             IActionContextAccessor actionContextAccessor, IQuizAnswersRepository quizAnswersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _quizCategoryRepository = quizCategoryRepository;
            _quizzesRepository = quizzesRepository;
            _appDbContext = appDbContext;
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            quizzes = _appDbContext.Quizzes.Include(q => q.QuizCategory).ToList();
            _quizAnswersRepository = quizAnswersRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<QuizIndexVM> IndexAsync(int id)
        {
            var model = new QuizIndexVM()
            {
                Quizzes = quizzes.Where(q => q.QuizCategoryId == id).ToList()
            };

            foreach (var item in model.Quizzes)
            {
                item.QuizDescription = null;
            }
            return model;
        }

        public async Task<QuizIndexVM> CheckAnswerAsync(QuizIndexVM model, int id)
        {
            QuizIndexVM modelDb = new QuizIndexVM()
            {
                Quizzes = quizzes.Where(q => q.QuizCategoryId == id).ToList()
            };

            model.Quizzes = modelDb.Quizzes;
            model.Errors = new Dictionary<int, string>();

            foreach (KeyValuePair<int, string> item in model.Answers)
            {
                Quiz quiz = modelDb.Quizzes.First(q => q.Id == item.Key);
                if (quiz.CorrectVariant == item.Value)
                {
                    model.Errors.Add(item.Key, quiz.CorrectVariant + " Cavabı Doğrudur");
                    model.CorrectCount++;
                }
                else
                {
                    model.Errors.Add(item.Key, item.Value + " Cavab Yanlışdır,  Düzgün cavab: " + quiz.CorrectVariant);
                    model.WrongCount++;
                }
            }

            int count = _modelstate.ErrorCount;

            QuizAnswer quizAnswer = new QuizAnswer
            {
                CreateAt = DateTime.Now,
                CorrectCount = model.CorrectCount,
                WrongCount = model.WrongCount,
                UserName = _httpContextAccessor.HttpContext.User.Identity.Name
            };

            await _quizAnswersRepository.CreateAsync(quizAnswer);

            return model;
        }
    }
}
