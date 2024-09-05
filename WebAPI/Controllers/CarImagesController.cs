using Business.Abstract;
using Core.Utilities.Results;
using Entity.Concrete;
using HeyRed.Mime;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Utilities;
using WebAPI.Utilities.Constants;
using System.IO;
using Autofac.Core;
using Core.Entities;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using Core.Utilities.Helpers.Abstracts;
using Entity.DTOs.Request;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //file işlemlerini halledecek ekstra helper class eklenecek
    //bir daha new car image olusturmak yerine dto kullan
    //Separation of Concerns: Move file handling logic to a separate service class. This keeps your controller focused on handling HTTP requests and responses.
    //Asynchronous Methods: Use asynchronous methods for file operations and service calls to improve performance and scalability.
    //Error Handling: Improve error handling to provide more specific feedback.
    //DTOs(Data Transfer Objects): Use DTOs for request and response models to decouple the API from the internal CarImage entity.
    //Service Layer: Ensure that your ICarImageService interface and its implementation handle business logic and data access.
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] AddCarImageRequestDTO request)
        {
            // create the carImage object
            // Add to the repository
            var result = _carImageService.Add(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("openimage")]
        public IActionResult OpenImage(int id)
        {
            var fileStream = _carImageService.GetFileStreamToOpen(id);
            var contentType = _carImageService.GetImageType(id);
            if (fileStream.IsSuccess && contentType.IsSuccess)
            {
                return File(fileStream.Data, contentType.Data);
            }
            return BadRequest();
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _carImageService.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm] UpdateCarImageRequestDTO request)
        {
            var result = _carImageService.Update(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var deleteresult = _carImageService.Delete(carImage);
            if (deleteresult.IsSuccess)
            {
                return Ok(deleteresult);
            }
            else
            {
                return BadRequest(deleteresult);
            }
        }
        
    }
}
