using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.Abstracts
{
    public interface IFileHelper
    {
        IResult Add(IFormFile file,string fileDirectory,string filePath);
        IResult Update(IFormFile file,string fileDirectory,string oldFilePath, string newFilePath);
        IResult Delete(string fileDirectory,string filePath);
        IDataResult<FileStream> GetFileStreamToOpen(string fileDirectory, string filePath);
    }
}
