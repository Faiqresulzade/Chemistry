using Core.Entities;
using Core.Utilities;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Resource;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        protected readonly IWebHostEnvironment _webHostEnvironment;


        public ResourceService(IResourceRepository resourceRepository, IActionContextAccessor actionContextAccessor, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _resourceRepository = resourceRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResourceIndexVM> IndexAsync()
        {
            var model = new ResourceIndexVM()
            {
                Resources = await _resourceRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(ResourceCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            if (!_fileService.IsImage(model.Image))
            {
                _modelState.AddModelError("Image", "Yüklənən fayl image formatında olmalıdır!");
                return false;
            }
            if (model.Pdf != null)
            {
                if (!_fileService.IsPdf(model.Pdf))
                {
                    _modelState.AddModelError("Pdf", "Yüklənən fayl Pdf formatında olmalıdır!");
                    return false;
                }
                model.PdfeUrl = await _fileService.Upload(model.Pdf, _webHostEnvironment.WebRootPath);
            }

            model.ImageUrl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);

            var resource = new Resource()
            {
                CreateAt = DateTime.Now,
                Image = model.ImageUrl,
                Link = model.Link,
                Pdf = model.PdfeUrl,
                Title = model.Title
            };

            await _resourceRepository.CreateAsync(resource);

            return true;
        }

        public async Task<ResourceUpdateVM> UpdateAsync(int id)
        {
            if (!_modelState.IsValid) return null;

            var resource = await _resourceRepository.GetAsync(id);
            if (resource == null)
                return null;

            return new ResourceUpdateVM
            {
                ImageUrl = resource.Image,
                Link = resource.Link,
                PdfeUrl = resource.Pdf,
                Title = resource.Title,
            }; ;
        }

        public async Task<bool> UpdateAsync(int id, ResourceUpdateVM model)
        {
            if (_modelState.IsValid) return false;

            var resource = await _resourceRepository.GetAsync(id);
            if (resource == null)
                return false;

            if (model.Image != null)
            {
                if (!_fileService.IsImage(model.Image))
                {
                    _modelState.AddModelError("Image", "Yüklənən fayl image formatında olmalıdır!");
                    return false;
                }
                model.ImageUrl = await _fileService.Upload(model.Image, _webHostEnvironment.WebRootPath);
            }

            if (model.Pdf != null)
            {
                if (_fileService.IsPdf(model.Pdf))
                {
                    _modelState.AddModelError("Pdf", "Yüklənən fayl Pdf formatında olmalıdır!");
                    return false;
                }
                model.PdfeUrl = await _fileService.Upload(model.Pdf, _webHostEnvironment.WebRootPath);
            }

            resource.Title = model.Title;
            resource.ModifiedAt = DateTime.Now;
            resource.Pdf = model.PdfeUrl;
            resource.Image = model.ImageUrl;
            resource.Link = model.Link;
            return true;
        }

        public async Task<Resource> GetDeleteAsync(int id) => await _resourceRepository.GetAsync(id);

        public async Task<bool> DeleteAsync(int id)
        {
            var resource = await _resourceRepository.GetAsync(id);

            if (resource == null) return false;

            _fileService.Delete(_webHostEnvironment.WebRootPath, resource.Pdf);
            _fileService.Delete(_webHostEnvironment.WebRootPath, resource.Image);

            await _resourceRepository.DeleteAsync(resource);

            return true;
        }

    }
}
