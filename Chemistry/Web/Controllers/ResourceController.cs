using DataAcces.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var resources=await _resourceRepository.GetAllAsync();
            if (resources == null) return View();
            return View(resources);
        }
    }
}
