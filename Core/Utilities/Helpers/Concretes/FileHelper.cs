using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.Results;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.Concretes
{
    public class FileHelper : IFileHelper
    {
        public IResult Add(IFormFile file,string fileDirectory,string filePath)
        {
            var fullFilePath = Path.Combine(fileDirectory, filePath);
            SaveFile(file,fileDirectory, fullFilePath);
            return new SuccessResult();
        }
        public IResult Update(IFormFile file, string fileDirectory,string oldFilePath,string newFilePath)
        {
            Delete(fileDirectory, oldFilePath);
            Add(file,fileDirectory, newFilePath);
            return new SuccessResult(); 
        }
        public IResult Delete(string fileDirectory, string filePath)
        {
            if (filePath != null)
            {
                var oldFilePath = Path.Combine(fileDirectory, filePath);
                DeleteFile(oldFilePath);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        public IDataResult<FileStream> GetFileStreamToOpen(string fileDirectory, string filePath)
        {
            var FullfilePath = Path.Combine(fileDirectory, filePath);
            if (!System.IO.File.Exists(FullfilePath))
            {
                return new ErrorDataResult<FileStream>("file doesnt exists");
            }
            var fileStream = new FileStream(FullfilePath, FileMode.Open, FileAccess.Read);
            // Return the file as a FileStreamResult
            return new SuccessDataResult<FileStream> (fileStream);
            
        }
        public static string GenerateFileName(IFormFile file)
        {
            var guid = Guid.NewGuid();
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{guid}{fileExtension}";
            return fileName;
        }
        private static void SaveFile(IFormFile file, string fileDirectory, string filePath)
        {
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);
        }
        private static void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

       
    }
}
