using Web.ViewModels.QuizCategory;

namespace Web.Services.Abstract
{
    public interface IQuizCategoryService
    {
        Task<QuizCategoryIndexVM> Index();
    }
}
