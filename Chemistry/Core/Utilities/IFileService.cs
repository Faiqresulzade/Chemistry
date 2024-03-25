using Microsoft.AspNetCore.Http;

namespace Core.Utilities
{
    public interface IFileService
    {
        Task<string> Upload(IFormFile file, string webRootPath);
        void Delete(string webRootPath, string fileName);
        bool IsImage(IFormFile file);
        bool IsPdf(IFormFile file);
        bool CheckSize(IFormFile file, int size);
    }
}
