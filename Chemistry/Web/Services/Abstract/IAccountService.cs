using Web.ViewModels.Account;

namespace Web.Services.Abstract
{
    public interface IAccountService
    {
        public Task<bool> Register(AccountRegisterVM model);
        public Task<bool> Login(AccountLoginVM model);
        public Task<bool> ResetPasswordAsync(AccountResetPasswordVM resetPasswordVM);
        public Task LogOut();
    }
}
