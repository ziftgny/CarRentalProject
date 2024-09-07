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
        IDataResult<List<GetCarImageResponseDTO>> GetALLByCarId(int carId);
        IDataResult<List<GetCarImageResponseDTO>> GetAll();
        IResult Add(AddCarImageRequestDTO request);
        IResult Delete(int id);
        IResult Update(UpdateCarImageRequestDTO request);
        IDataResult<OpenImageResponseDTO> OpenImage(int id);
    }
}
