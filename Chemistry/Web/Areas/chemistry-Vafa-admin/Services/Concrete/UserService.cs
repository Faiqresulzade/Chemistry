using Core.Entities;
using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class UserService : IUsersService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> IndexAsync() => await _appDbContext.Users.ToListAsync();

        public async Task<bool> DeleteAsync(string id)
        {
            User user = await _appDbContext.Users.FirstOrDefaultAsync(u=>u.Id==id);

            if (user == null) return false;

            _appDbContext.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
