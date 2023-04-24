using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IAccountService
    {
        public Task<HomeRegisterVM> Register(HomeRegisterVM model);
        public Task<HomeIndexVM> Login(HomeIndexVM model);
    }
}
