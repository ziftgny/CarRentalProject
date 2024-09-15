using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs.Request;
using Entity.DTOs.Response;
using HeyRed.Mime;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;
        IMapper _mapper;
        ICarService _carService;
        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper, IMapper mapper, ICarService carService)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _mapper = mapper;
            _carService = carService;
        }
        [SecuredOperation("carimage.add,admin")]
        [ValidationAspect(typeof(AddCarImageRequestDTOValidator))]
        public IResult Add(AddCarImageRequestDTO request)
        {
            IResult result = BusinessRules.Run(CheckIfCarIdExists(request.CarId),
                CheckIfCarImagesExceedsCapacity(request.CarId));
            if (result == null)
            {
                var carImage = _mapper.Map<CarImage>(request);
                _carImageDal.Add(carImage);
                _fileHelper.Add(request.ImageFile, FilePaths.ImagesFolder, carImage.ImagePath);
                return new SuccessResult();
            }
            return result;
        }
        [SecuredOperation("carimage.delete,admin")]
        public IResult Delete(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageIdExists(id));
            if (result == null)
            {
                var carImage = _carImageDal.Get(i => i.Id == id);
                var pathToBeDeleted = carImage.ImagePath;
                _fileHelper.Delete(FilePaths.ImagesFolder, pathToBeDeleted);
                _carImageDal.Delete(carImage);
                return new SuccessResult();
            }
            return result;
        }

        public IDataResult<List<GetCarImageResponseDTO>> GetAll()
        {
            var carImages = _mapper.Map<List<GetCarImageResponseDTO>>(_carImageDal.GetAll());
            return new SuccessDataResult<List<GetCarImageResponseDTO>>
                (carImages);
        }
        public IDataResult<List<GetCarImageResponseDTO>> GetALLByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Count == 0)
            {
                var dto = new GetCarImageResponseDTO { ImagePath = "default.png" };
                return new ErrorDataResult<List<GetCarImageResponseDTO>>(new List<GetCarImageResponseDTO>() { dto });
            }
            var carImages = _mapper.Map<List<GetCarImageResponseDTO>>(result);
            return new SuccessDataResult<List<GetCarImageResponseDTO>>(carImages);
        }
        public IDataResult<GetCarImageResponseDTO> GetById(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageIdExists(id));
            if (result == null)
            {
                var carImage = _mapper.Map<GetCarImageResponseDTO>(_carImageDal.Get(i => i.Id == id));
                return new SuccessDataResult<GetCarImageResponseDTO>(carImage);
            }
            return new ErrorDataResult<GetCarImageResponseDTO>(Messages.CarImageIdDoesntExists);
        }
        [SecuredOperation("carimage.update,admin")]
        [ValidationAspect(typeof(UpdateCarImageRequestDTOValidator))]
        public IResult Update(UpdateCarImageRequestDTO request)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageIdExists(request.CarImageId));
            if (result == null)
            {
                var oldCarImage = _carImageDal.Get(p => p.Id == request.CarImageId);
                var pathToBeDeleted = oldCarImage.ImagePath;
                var carImage = _mapper.Map<UpdateCarImageRequestDTO, CarImage>(request, oldCarImage);
                var pathToBeCreated = carImage.ImagePath;
                _fileHelper.Update(request.ImageFile, FilePaths.ImagesFolder, pathToBeDeleted, pathToBeCreated);
                _carImageDal.Update(carImage);
                return new SuccessResult();
            }
            return result;
        }

        public IDataResult<OpenImageResponseDTO> OpenImage(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageIdExists(id));
            if (result == null)
            {
                var carImage = _carImageDal.Get(i => i.Id == id);
                var imageType = MimeTypesMap.GetMimeType(Path.GetExtension(carImage.ImagePath));
                var fileStream = _fileHelper.GetFileStreamToOpen(FilePaths.ImagesFolder, carImage.ImagePath).Data;
                return new SuccessDataResult<OpenImageResponseDTO>(new OpenImageResponseDTO
                {
                    FileStream = fileStream,
                    ImageType = imageType
                });
            }
            return new ErrorDataResult<OpenImageResponseDTO>(Messages.CarImageIdDoesntExists);
        }
        private IResult CheckIfCarIdExists(int id)
        {
            var result = _carService.GetByID(id);
            if (result == null)
            {
                return new ErrorResult(Messages.CarIdDoesntExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCarImageIdExists(int carImageId)
        {
            var result = _carImageDal.Get(c => c.Id == carImageId);
            if (result == null)
            {
                return new ErrorResult(Messages.CarImageIdDoesntExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCarImagesExceedsCapacity(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Count >= 5)
            {
                return new ErrorResult(Messages.MaxCarImage);
            }
            return new SuccessResult();
        }


    }
}
