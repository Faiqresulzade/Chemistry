using Core.Entities;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.CurseInfo;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class CurseInfoService : ICurseInfoService
    {
        #region Configuration
        private readonly ICurseInfoRepository _curseInfoRepository;
        private readonly ModelStateDictionary _modelState;
        
        public CurseInfoService(ICurseInfoRepository curseInfoRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _curseInfoRepository = curseInfoRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        #endregion
        #region Index
        public async Task<CurseInfoIndexVM> IndexAsync()
        {
            var model = new CurseInfoIndexVM()
            {
                Curseİnfos = await _curseInfoRepository.FirstorDefaultAsync()
            };
            return model;
        }
        #endregion
        #region Create
        public async Task<bool> CreateAsync(CurseInfoCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            //var curseInfo=await _curseInfoRepository.FirstorDefaultAsync();
            //curseInfo.CreateAt = DateTime.Now;
            //curseInfo.Experience = model.CurseInfos.Experience;
            //curseInfo.Graduates = model.CurseInfos.Graduates;
            //curseInfo.Test = model.CurseInfos.Test;
            //curseInfo.VideoLesson=model.CurseInfos.VideoLesson;
            Curseİnfo curseInfo = new Curseİnfo()
            {
                CreateAt=DateTime.Now,
                Experience=model.CurseInfos.Experience,
                Test=model.CurseInfos.Test,
                VideoLesson=model.CurseInfos.VideoLesson,
                Graduates=model.CurseInfos.Graduates,
            };
            await _curseInfoRepository.CreateAsync(curseInfo);
            return true;
        }

        #endregion
        #region Update
        public async Task<CurseInfoUpdateVM> UpdateAsync(int id)
        {
            var curseInfo = await _curseInfoRepository.GetAsync(id);
            if (curseInfo == null) return null;

            CurseInfoUpdateVM model = new CurseInfoUpdateVM()
            {
                Curseİnfos=curseInfo
            };
            //model.Curseİnfos.VideoLesson = curseInfo.VideoLesson;
            //model.Curseİnfos.Test = curseInfo.Test;
            //model.Curseİnfos.Graduates = curseInfo.Graduates;
            //model.Curseİnfos.Experience=curseInfo.Experience;
            return model;
        }

        public async Task<bool> UpdateAsync(int id, CurseInfoUpdateVM model)
        {
            var curseInfo = await _curseInfoRepository.GetAsync(id);
            if(curseInfo == null) return false;
            curseInfo.Experience = model.Curseİnfos.Experience;
            curseInfo.VideoLesson = model.Curseİnfos.VideoLesson;
            curseInfo.Test = model.Curseİnfos.Test;
            curseInfo.ModifiedAt = DateTime.Now;
            curseInfo.Graduates = model.Curseİnfos.Graduates;
            await _curseInfoRepository.SaveChanges();
            return true;
        }
        #endregion
        #region Delete
        public async Task<Curseİnfo> DeleteAsync(int id)
        {
            var curseInfo=await _curseInfoRepository.GetAsync(id);
            return curseInfo;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
           var curseInfo=await _curseInfoRepository.GetAsync(id);
            if (curseInfo == null) return false;
            await _curseInfoRepository.DeleteAsync(curseInfo);
            return true;
        }
        #endregion
    }
}
