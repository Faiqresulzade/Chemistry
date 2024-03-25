using DataAcces.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.CurseInfo;

namespace Web.Areas.chemistry_Vafa_admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("chemistry-Vafa-admin")]
    public class CurseInfoController : Controller
    {
        private readonly ICurseInfoService _curseInfoService;
        private readonly AppDbContext _appDbContext;

        public CurseInfoController(ICurseInfoService curseInfoService,AppDbContext appDbContext)
        {
            _curseInfoService = curseInfoService;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var curseInfo = await _curseInfoService.IndexAsync();
            if (curseInfo == null) return View();
            return View(curseInfo);
        }

        public async Task<IActionResult> Create()
        {
            var curseInfo = await _appDbContext.Curseİnfos.FirstOrDefaultAsync();
            if (curseInfo != null) return BadRequest();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CurseInfoCreateVM model)
        {
            bool isExist = await _curseInfoService.CreateAsync(model);
            if (isExist) return RedirectToAction(nameof(Index));
            return View(model);
        }

        public async Task<IActionResult>Update(int id)
        {
            var curseInfo = await _curseInfoService.UpdateAsync(id);
            if (curseInfo == null) return BadRequest();
            return View(curseInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, CurseInfoUpdateVM model)
        {
            bool isExist=await _curseInfoService.UpdateAsync(id, model);
            if (!isExist) return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Delete(int id)
        {
            var curseInfo=await _curseInfoService.DeleteAsync(id);
            if(curseInfo == null) return BadRequest();
            return View(curseInfo);
        }
        [HttpPost]
        public async Task<IActionResult>DeletePost(int id)
        {
            bool isExist=await _curseInfoService.DeletePostAsync(id);
            if (!isExist) return BadRequest();
            return RedirectToAction(nameof(Index));
        }
    }
}
