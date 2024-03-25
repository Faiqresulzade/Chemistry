using Core.Entities;
using DataAcces.Context;
using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concret
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurseInfoRepository _curseInfoRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly ModelStateDictionary _modelstate;
        private static bool SendMessage;
        public HomeService(AppDbContext appDbContext,
            IActionContextAccessor actionContextAccessor,
            ICurseInfoRepository curseInfoRepository,
            IResourceRepository resourceRepository)
        {
            _appDbContext = appDbContext;
            _curseInfoRepository = curseInfoRepository;
            _resourceRepository = resourceRepository;
            _modelstate = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<HomeIndexVM> IndexAsync()
        {
            var model = new HomeIndexVM()
            {
                GetPersonInfo = await _appDbContext.PersonInfo.FirstOrDefaultAsync(),
                Students = await _appDbContext.Students.ToListAsync(),
                News = await _appDbContext.News
                                        .OrderByDescending(n => n.Id)
                                        .Take(2)
                                        .ToListAsync(),
                HomeSliders = await _appDbContext.HomeSliders.ToListAsync(),
                Curseİnfo = await _curseInfoRepository.FirstorDefaultAsync(),
                Resources = await _resourceRepository.GetAllAsync()
            };
            // model.MesageSended = false;
            if (SendMessage)
            {
                model.MesageSended = true;
                SendMessage = false;
            }
            return model;
        }
        public async Task<HomeMessageVM> MessageAsync(HomeMessageVM message)
        {
            if (!_modelstate.IsValid) return null;
            var messages = new Message()
            {
                CreateAt = DateTime.Now,
                Description = message.Message,
                Email = message.Email,
                Name = message.Name,
            };
            await _appDbContext.Messages.AddAsync(messages);
            await _appDbContext.SaveChangesAsync();
            SendMessage = true;
            return message;
        }
    }
}
