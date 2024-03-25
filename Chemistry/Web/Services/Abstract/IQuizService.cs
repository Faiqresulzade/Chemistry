using Web.ViewModels.Quiz;

namespace Web.Services.Abstract
{
    public interface IQuizService
    {
        Task<QuizIndexVM> IndexAsync(int id);
        Task<QuizIndexVM> CheckAnswerAsync(QuizIndexVM model, int id);
    }
}
