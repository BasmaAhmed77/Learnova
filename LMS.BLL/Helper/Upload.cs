using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.Helper
{
    public class Upload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Upload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", folderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public void DeleteFile(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
