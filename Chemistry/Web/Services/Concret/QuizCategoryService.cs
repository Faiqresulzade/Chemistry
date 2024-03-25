using DataAcces.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels.QuizCategory;

namespace Web.Services.Concret
{
    public class QuizCategoryService : IQuizCategoryService
    {
        private readonly IQuizCategoryRepository _quizCategoryRepository;

        public QuizCategoryService(IQuizCategoryRepository quizCategoryRepository)
        {
            _quizCategoryRepository = quizCategoryRepository;
        }
        public async Task<QuizCategoryIndexVM> Index()
        {
            var model = new QuizCategoryIndexVM()
            {
                QuizCategories = await _quizCategoryRepository.GetAllAsync()
            };
            return model;
        }
    }
}
