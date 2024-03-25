using Core.Entities;

namespace Web.Services.Abstract
{
    public interface IUsersService
    {
        Task<List<User>> IndexAsync();
        Task<bool>DeleteAsync(string id);
    }
}
