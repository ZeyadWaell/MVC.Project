using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Demo.NL.Helper
{
	public class DouctumersSettings
	{
        public static string UploadFile(IFormFile file, string foldername)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension.ToLower()))
                throw new ArgumentException("Invalid file extension");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImageFolder", foldername);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName;
        }
    }
}
