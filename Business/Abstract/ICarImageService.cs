using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs.Request;
using Entity.DTOs.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<GetCarImageResponseDTO> GetById(int id);
        IDataResult<List<GetCarImageResponseDTO>> GetAll();
        IResult Add(AddCarImageRequestDTO request);
        IResult Delete(CarImage carImage);
        IResult Update(UpdateCarImageRequestDTO request);
        IDataResult<string> GetImageType(int id);
        IDataResult<FileStream> GetFileStreamToOpen(int id);
    }
}
