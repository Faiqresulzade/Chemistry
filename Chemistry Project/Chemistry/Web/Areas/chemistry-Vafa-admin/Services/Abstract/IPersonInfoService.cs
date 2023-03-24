using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IPersonInfoService
    {
        Task<PersonInfoIndexVM> IndexAsync();
        Task<bool> CreateAsync(PersonInfoCreateVM model);
    }
}
