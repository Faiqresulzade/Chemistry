using DataAcces.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class PersonInfoController : Controller
    {
        private readonly IPersonInfoService _personInfo;
        private readonly AppDbContext _appDbContext;

        public PersonInfoController(IPersonInfoService personInfo,AppDbContext appDbContext)
        {
            _personInfo = personInfo;
            _appDbContext = appDbContext;
        }

        #region crud
        public async Task<IActionResult> Index()
        {
            var personInfo =await _personInfo.IndexAsync();

            return View(personInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var personInfo = await _appDbContext.PersonInfo.FirstOrDefaultAsync();
            if(personInfo == null) return View();
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonInfoCreateVM model)
        {
            bool result =await _personInfo.CreateAsync(model);
            if (!result) return View(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var personInfo=await _personInfo.GetPersonInfoUpdateAsync(id);
            if (personInfo == null) return NotFound();
            return View(personInfo);
        }

        [HttpPost]

        public async Task<IActionResult>Update(PersonInfoUpdateVM model, int id)
        {
            var personInfo = await _personInfo.UpdateAsync(id,model);
            if (personInfo) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult>GetDelete(int id)
        {
            var personInfo =await _personInfo.GetDeleteAsync(id);
            if (personInfo == null) return NotFound();
            return View(personInfo);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            bool isExist =await _personInfo.DeleteAsync(id);
            if(isExist) return RedirectToAction(nameof(Index));
            return NotFound();
        }

        #endregion
    }
}
