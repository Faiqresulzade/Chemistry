using Microsoft.AspNetCore.Mvc;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Students;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Area("chemistry-Vafa-admin")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _students;

        public StudentsController(IStudentsService students)
        {
            _students = students;
        }
      
        public async Task<IActionResult> Index()
        {
            var students =await _students.GetStudents();
            if (students == null) return View();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentsCreateVM model)
        {
            bool isExist =await _students.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult>Update(int id)
        {
            var students = await _students.UpdateAsync(id);

            if (students == null) return NotFound();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult>Update(StudentsUpdateVM model, int id)
        {
            bool isExist=await _students.UpdateAsync(model, id);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult>GetDelete(int id)
        {
            var students = await _students.GetDeletAsync(id);
            if (students == null) return NotFound();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            bool isExist = await _students.Delete(id);
            if (isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
