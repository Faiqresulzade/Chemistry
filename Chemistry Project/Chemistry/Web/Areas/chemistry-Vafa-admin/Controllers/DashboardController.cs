using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Area("chemistry-Vafa-admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
