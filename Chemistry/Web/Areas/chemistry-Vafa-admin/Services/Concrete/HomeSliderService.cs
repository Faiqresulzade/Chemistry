using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.HomeSlider;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class HomeSliderService : BaseService, IHomeSliderService
    {
        #region Configuration
        public HomeSliderService(
        IHomeSliderRepository homeSliderRepository,
        IActionContextAccessor actionContextAccessor,
        IWebHostEnvironment webHostEnvironment,
        IFileService fileService)
        : base(actionContextAccessor, homeSliderRepository, webHostEnvironment, fileService) { }
        #endregion
        #region Index
        public async Task<HomeSliderIndexVM> IndexAsync()
        {
            var model = new HomeSliderIndexVM
            {
                HomeSliders = await _homeSliderRepository.GetAllAsync()
            };
            return model;
        }
        #endregion
        #region Create
        public async Task<bool> CreateAsync(HomeSliderCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            if (model.BackRoundImage != null)
            {
                if (!_fileService.IsImage(model.BackRoundImage))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.BackRoundImage, 260))
                {
                    _modelstate.AddModelError("Photo", "Şəkilin ölçüsü 260KB-dan böyükdür!!");
                    return false;
                }
                model.BackRoundImagePath = await _fileService.Upload(model.BackRoundImage, _webHostEnvironment.WebRootPath);
            }

            var homeSlider = new HomeSlider
            {
                CreateAt = DateTime.Now,
                Category = model.Category,
                Description = model.Description,
                Title = model.Title,
                BackRoundImage = model.BackRoundImagePath
            };
            await _homeSliderRepository.CreateAsync(homeSlider);
            return true;
        }

        #endregion
        #region Update
        public async Task<HomeSliderUpdateVM> UpdateAsync(int id)
        {
            var slider = await _homeSliderRepository.GetAsync(id);
            if (slider == null)
            {
                _modelstate.AddModelError("Title", "Bele tittle movcud deyil!!");
                return null;
            }

            var model = new HomeSliderUpdateVM
            {
                BackRoundImagePath = slider.BackRoundImage,
                Category = slider.Category,
                Description = slider.Description,
                Title = slider.Title
            };
            return model;
        }

        public async Task<bool> UpdateAsync(int id, HomeSliderUpdateVM model)
        {

            if (!_modelstate.IsValid) return false;
            var slider = await _homeSliderRepository.GetAsync(id);
            if (slider == null) return false;
            if (model.BackRoundImage != null)
            {
                if (!_fileService.IsImage(model.BackRoundImage))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.BackRoundImage, 260))
                {
                    _modelstate.AddModelError("Photo", "Şəkilin ölçüsü 260KB-dan böyükdür!!");
                    return false;
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, slider.BackRoundImage);
                slider.BackRoundImage = await _fileService.Upload(model.BackRoundImage, _webHostEnvironment.WebRootPath);
            }

            slider.Title = model.Title;
            slider.Category = model.Category;
            slider.ModifiedAt = DateTime.Now;
            slider.Description = model.Description;
            await _homeSliderRepository.SaveChanges();
            return true;
        }

        #endregion
        #region Delete
        public async Task<HomeSlider> GetDelete(int id)
        {
            var slider = await _homeSliderRepository.GetAsync(id);
            return slider;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var slider = await _homeSliderRepository.GetAsync(id);
            if (slider == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, slider.BackRoundImage);
            await _homeSliderRepository.DeleteAsync(slider);
            return true;
        }
        #endregion
    }

    public class BaseService
    {
        protected readonly ModelStateDictionary _modelstate;
        protected readonly IHomeSliderRepository _homeSliderRepository;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        protected readonly IFileService _fileService;

        public BaseService(
            IActionContextAccessor actionContextAccessor,
            IHomeSliderRepository homeSliderRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _homeSliderRepository = homeSliderRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
    }
}
