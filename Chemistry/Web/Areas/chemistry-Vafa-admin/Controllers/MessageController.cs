using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<IActionResult> Index()
        {
            var message =await _messageService.Messages();
            if (message == null) return View();
            return View(message);
        }
        public async Task<IActionResult> Delete(int id)
        {
             bool isExist=await _messageService.DeleteAsync(id);
            if (isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
