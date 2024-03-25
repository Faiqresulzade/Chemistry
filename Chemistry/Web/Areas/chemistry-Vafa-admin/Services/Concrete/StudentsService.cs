using Core.Entities;
using Core.Utilities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.chemistry_Vafa_admin.Services.Abstract;
using Web.Areas.chemistry_Vafa_admin.ViewModels.Students;

namespace Web.Areas.chemistry_Vafa_admin.Services.Concrete
{
    public class StudentsService : IStudentsService
    {
        #region Configuration
        private readonly ModelStateDictionary _modelstate;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        public StudentsService(
            IStudentsRepository studentsRepository,
            IActionContextAccessor actionContextAccessor,
            IWebHostEnvironment webHostEnvironment,
            AppDbContext appDbContext,
            IFileService fileService)
        {
            _modelstate = actionContextAccessor.ActionContext.ModelState;
            _studentsRepository = studentsRepository;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _fileService = fileService;
        }
        #endregion
        #region GetStudents
        public async Task<StudentsIndexVM> GetStudents()
        {
            var model = new StudentsIndexVM
            {
                Students = await _studentsRepository.GetAllAsync()
            };
            return model;
        }
        #endregion
        #region Create
        public async Task<bool> CreateAsync(StudentsCreateVM model)
        {
            if (!_modelstate.IsValid) return false;
            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yuklenen sekil image formatinda olmalidir!!");
                    return false;
                }
                //if (!_fileService.CheckSize(model.Photo,))
                //{
                //    _modelstate.AddModelError("Photo", "sekiln olcusu 60kbdan boyukdur!!");
                //    return false;
                //}
                model.PhotoPath = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }
            var student = new Students
            {
                FullName = model.FullName,
                CreateAt = DateTime.Now,
                Point = model.Point,
                Profession = model.Profession,
                University = model.University,
                Photo = model.PhotoPath
            };
            await _studentsRepository.CreateAsync(student);
            return true;
        }
        #endregion
        #region Update
        public async Task<StudentsUpdateVM> UpdateAsync(int id)
        {
            var students = await _studentsRepository.GetAsync(id);
            if (students == null)
            {
                _modelstate.AddModelError("Title", "Şagird mövcud deyil!!!, yenidən cəhd edin");
                return null;
            }
            var model = new StudentsUpdateVM
            {
                FullName = students.FullName,
                ModifiedAt = DateTime.Now,
                Point = students.Point,
                Profession = students.Profession,
                University = students.University,
                PhotoPath = students.Photo
            };
            return model;
        }

        public async Task<bool> UpdateAsync(StudentsUpdateVM model, int id)
        {
            if (!_modelstate.IsValid) return false;
            var students = await _studentsRepository.GetAsync(id);
            if (students == null) return false;

            //bool isExist = await _studentsRepository.AnyAsync(s =>
            //s.FullName.ToLower().Trim() ==
            //model.FullName.ToLower().Trim() && s.Id != model.Id);

            //if (isExist)
            //{
            //    _modelstate.AddModelError("FullName", "bu adda students movcuddur!!");
            //    return false;
            //}

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelstate.AddModelError("Photo", "Yüklənən şəkil image formatında olmalıdır!!");
                    return false;
                }
                if (!_fileService.CheckSize(model.Photo, 200))
                {
                    _modelstate.AddModelError("Photo", "şəkilin ölçüsü 200kb-dan böyükdür!!");
                    return false;
                }
                _fileService.Delete(_webHostEnvironment.WebRootPath, students.Photo);
                students.Photo = await _fileService.Upload(model.Photo, _webHostEnvironment.WebRootPath);
            }
            students.FullName = model.FullName;
            students.Profession = model.Profession;
            students.ModifiedAt = DateTime.Now;
            students.University = model.University;
            students.Point = model.Point;
            await _studentsRepository.SaveChanges();
            return true;
        }

        #endregion
        #region delete
        public async Task<Students> GetDeletAsync(int id)
        {
            var students = await _studentsRepository.GetAsync(id);
            return students;
        }
        public async Task<bool> Delete(int id)
        {
            var students = await _studentsRepository.GetAsync(id);
            if (students == null) return false;
            _fileService.Delete(_webHostEnvironment.WebRootPath, students.Photo);
            await _studentsRepository.DeleteAsync(students);
            return true;
        }
        #endregion
    }
}