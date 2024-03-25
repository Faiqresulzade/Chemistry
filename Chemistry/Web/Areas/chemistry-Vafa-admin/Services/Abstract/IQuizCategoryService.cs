using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.QuizCategory;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IQuizCategoryService
    {
        Task<QuizCategoryIndexVM> IndexAsync();
        Task<bool> CreateAsync(QuizCategoryCreatVM model);
        Task<QuizCategoryUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id,QuizCategoryUpdateVM model);
        Task<QuizCategory> GetDeleteAsync(int id);
        Task<bool>DeleteAsync(int id);

    }
}
