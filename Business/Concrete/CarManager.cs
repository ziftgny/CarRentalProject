using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            //validation ekle
            _carDal.Add(car);
            return new SuccessResult();

        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.delete,admin")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()   
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }
        [CacheAspect]
        public IDataResult<Car> GetByID(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(p=>p.Id==id));
        }
        [CacheAspect]
        public IDataResult<List<CarDetailsDTO>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.GetCarDetails());
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p=>p.BrandId == id));
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p=>p.ColorId == id));
        }
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.update,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
