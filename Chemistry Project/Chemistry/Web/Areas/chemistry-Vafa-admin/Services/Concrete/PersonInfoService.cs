using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class PersonInfoService :IPersonInfoService
    {
        private readonly ModelStateDictionary _modelstate;
        private readonly IPersonInfoRepository _personInfoRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;

        public PersonInfoService(
            IPersonInfoRepository personInfoRepository,
            IActionContextAccessor actionContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext,
            IFileService fileService)
        {
            _personInfoRepository = personInfoRepository;
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _fileService = fileService;
        }

        #region Index
        public async Task<PersonInfoIndexVM> IndexAsync()
        {
            var model = new PersonInfoIndexVM
            {
                personInfo=await _personInfoRepository.FirstorDefaultAsync()
            };
            return model;
        }
        #endregion
        #region Create


        public async Task<bool> CreateAsync(PersonInfoCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 60))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 60kbdan boyukdur!!");
                    return false;
                }
            }
            model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);

            var personInfo = new PersonInfo
            {
                CreateAt = DateTime.Now,
                Text = model.Info,
                Title = model.FullName,
                Photo = model.PhotoPath,
            };
            await _personInfoRepository.CreateAsync(personInfo);
            return true;
        }


        #endregion
    }
}
