using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Core.Utilities
{
    public class FileService :IFileService
    {
        public async Task<string> Upload(IFormFile file,string webRootPath)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            var path = Path.Combine(webRootPath, "assets/media", fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public void Delete(string webRootPath,string fileName)
        {
            var path = Path.Combine(webRootPath, "assets/media", fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image/")) return true;
            return false;
        }

        public bool CheckSize(IFormFile file, int size)
        {
           return true;
        }

        public bool IsPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string extension = Path.GetExtension(fileName);

            return extension == ".pdf";
        }

    }
}
