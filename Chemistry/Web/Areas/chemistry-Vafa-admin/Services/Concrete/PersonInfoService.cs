using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.PersonInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class PersonInfoService :IPersonInfoService
    {
        #region Configuration
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
        #endregion
        #region Index
        public async Task<PersonInfoIndexVM> IndexAsync()
        {
            var model = new PersonInfoIndexVM
            {
                personInfo=await _personInfoRepository.FirstorDefaultAsync(),
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
                if (!_fileService.CheckSize(model.Photo, 160))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 60kbdan boyukdur!!");
                    return false;
                }
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }

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
        #region Update
        public async Task<PersonInfoUpdateVM> GetPersonInfoUpdateAsync(int id)
        {
            var personInfo =await _personInfoRepository.GetAsync(id);
            if (personInfo == null)
            {
                _modelstate.AddModelError("Title", "Bele PersonInfo movcud deyil!!");
                return null;
            }
            var model = new PersonInfoUpdateVM
            {
                FullName = personInfo.Title,
                Info = personInfo.Text,
                PhotoPath = personInfo.Photo,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(int id, PersonInfoUpdateVM model)
        {
            if (!_modelstate.IsValid) return false;
            var personInfo = await _personInfoRepository.GetAsync(id);
            if(personInfo == null)return false;

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 160))
                {
                    _modelstate.AddModelError("Photo", "sekiln olcusu 160kbdan boyukdur!!");
                    return false;
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath,personInfo.Photo);
                personInfo.Photo = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
                
            }
            personInfo.ModifiedAt = DateTime.Now;
            personInfo.Title = model.FullName;
            personInfo.Text = model.Info;
            await _personInfoRepository.SaveChanges();
            return true;
        }

        #endregion

        #region Delete
        public async Task<PersonInfo> GetDeleteAsync(int id)
        {
            var personInfo=await _personInfoRepository.GetAsync(id);
            return personInfo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personInfo = await _personInfoRepository.GetAsync(id);
            if (personInfo == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, personInfo.Photo);
            await _personInfoRepository.DeleteAsync(personInfo);
            return true;
        }
        #endregion
    }
}
