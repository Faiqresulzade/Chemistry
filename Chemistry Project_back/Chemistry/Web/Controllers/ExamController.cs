using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
