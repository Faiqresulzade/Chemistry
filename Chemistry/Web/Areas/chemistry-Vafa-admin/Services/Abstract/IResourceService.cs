using Core.Entities;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Resource;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IResourceService
    {
        Task<ResourceIndexVM> IndexAsync();
        Task<bool> CreateAsync(ResourceCreateVM model);
        Task<ResourceUpdateVM> UpdateAsync(int id);
        Task<bool> UpdateAsync(int id, ResourceUpdateVM model);
        Task<Resource> GetDeleteAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
