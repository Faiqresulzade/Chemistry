using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IPersonInfoService
    {
        Task<HomeIndexVM> IndexAsync();
    }
}
