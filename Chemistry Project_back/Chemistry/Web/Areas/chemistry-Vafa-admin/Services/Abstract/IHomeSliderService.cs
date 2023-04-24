using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.HomeSlider;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IHomeSliderService
    {
        Task<HomeSliderIndexVM> IndexAsync();
        Task<bool> CreateAsync(HomeSliderCreateVM model);
        Task<HomeSliderUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id,HomeSliderUpdateVM model);
        Task<HomeSlider> GetDelete(int id);
        Task<bool> DeleteAsync(int id);
    }
}
