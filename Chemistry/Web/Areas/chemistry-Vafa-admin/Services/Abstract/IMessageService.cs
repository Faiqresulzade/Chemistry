using Web.Areas.chemistry_Vafa_admin.ViewModels.Message;

namespace Web.Areas.chemistry_Vafa_admin.Services.Abstract
{
    public interface IMessageService
    {
        Task<MessageIndexVM> Messages();
        Task<bool> DeleteAsync(int id);
    }
}
