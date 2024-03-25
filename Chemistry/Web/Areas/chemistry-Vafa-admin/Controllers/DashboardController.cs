using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("chemistry-Vafa-admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
