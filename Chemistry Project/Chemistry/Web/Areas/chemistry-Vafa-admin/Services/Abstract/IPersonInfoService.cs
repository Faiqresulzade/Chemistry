using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IPersonInfoService
    {
        Task<PersonInfoIndexVM> IndexAsync();
        Task<bool> CreateAsync(PersonInfoCreateVM model);
        Task<PersonInfoUpdateVM> GetPersonInfoUpdateAsync(int id);
        Task<bool> UpdateAsync(int id, PersonInfoUpdateVM model);
        Task<PersonInfo> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
