using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Quiz;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IQuizService
    {
        Task<QuizIndexVM> IndexAsync();
        Task<QuizCreateVM> CreateAsync();
        Task<bool> CreateAsync(QuizCreateVM model);
        Task<QuizUpdateVM> Update(int id);
        Task<bool> Update(int id,QuizUpdateVM model);
        Task<Quiz> GetdeleteAsync(int id);
        Task<bool> DeleteAsync(int id);

        Task<Quiz> Details(int id);
    }
}
