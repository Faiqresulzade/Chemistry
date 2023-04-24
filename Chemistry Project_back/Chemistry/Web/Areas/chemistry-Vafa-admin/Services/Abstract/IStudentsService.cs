using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Students;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IStudentsService
    {
        Task<StudentsIndexVM> GetStudents();
        Task<bool> CreateAsync(StudentsCreateVM model);
        Task<StudentsUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(StudentsUpdateVM model, int id);
        Task<Students> GetDeletAsync(int id);
        Task<bool> Delete(int id);
    }
}
