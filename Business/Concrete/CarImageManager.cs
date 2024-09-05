using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs.Request;
using Entity.DTOs.Response;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        IMapper _mapper;
        public CarImageManager(ICarImageDal carImageDal,IFileHelper fileHelper,IMapper mapper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _mapper = mapper;
        }

        public IResult Add(AddCarImageRequestDTO request)
        {
            var carImage = _mapper.Map<CarImage>(request);
            _fileHelper.Add(request.ImageFile, FilePaths.ImagesFolder,carImage.ImagePath);
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var pathToBeDeleted = this.GetById(carImage.Id).Data.ImagePath;
            _fileHelper.Delete(FilePaths.ImagesFolder, pathToBeDeleted);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<GetCarImageResponseDTO>> GetAll()
        {
            var carImages = _mapper.Map<List<GetCarImageResponseDTO>>(_carImageDal.GetAll());
            return new SuccessDataResult<List<GetCarImageResponseDTO>>
                (carImages);
        }

        public IDataResult<GetCarImageResponseDTO> GetById(int id)
        {
            var carImage = _mapper.Map<GetCarImageResponseDTO>(_carImageDal.Get(i=>i.Id==id));
            return new SuccessDataResult<GetCarImageResponseDTO>
                (carImage);
        }
        public IResult Update(UpdateCarImageRequestDTO request)
        {
            var carImage = _mapper.Map<UpdateCarImageRequestDTO, CarImage>(request,
                _carImageDal.Get(p => p.Id == request.CarImageId)); ;
            var pathToBeDeleted = this.GetById(request.CarImageId).Data.ImagePath;
            var pathToBeCreated = carImage.ImagePath;
            _fileHelper.Update(request.ImageFile, FilePaths.ImagesFolder, pathToBeDeleted, pathToBeCreated);
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }
        public IDataResult<string> GetImageType(int id)
        {
            var imagePath = this.GetById(id).Data.ImagePath;
            return  new SuccessDataResult<string>(MimeTypesMap.GetMimeType(Path.GetExtension(imagePath)),default);
        }
        public IDataResult<FileStream> GetFileStreamToOpen(int id)
        {
            var imagePath = this.GetById(id).Data.ImagePath;
           return new SuccessDataResult<FileStream> (_fileHelper.GetFileStreamToOpen(FilePaths.ImagesFolder, imagePath).Data);
        }
    }
}
