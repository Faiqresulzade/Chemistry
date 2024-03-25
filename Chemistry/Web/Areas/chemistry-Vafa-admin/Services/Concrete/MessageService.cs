using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Message;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _appDbContext;

        public MessageService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<MessageIndexVM> Messages()
        {
            var model = new MessageIndexVM()
            {
                Messages = await _appDbContext.Messages.OrderByDescending(m=>m.Id).ToListAsync()
            };
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _appDbContext.Messages.FindAsync(id);
            if (message == null) return false;
            _appDbContext.Remove(message);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
