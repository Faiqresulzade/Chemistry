using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.CurseInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface ICurseInfoService
    {
        Task<CurseInfoIndexVM> IndexAsync();
        Task<bool> CreateAsync(CurseInfoCreateVM model);
        Task<CurseInfoUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id,CurseInfoUpdateVM model);
        Task<Curseİnfo> DeleteAsync(int id);
        Task<bool> DeletePostAsync(int id);
    }
}
