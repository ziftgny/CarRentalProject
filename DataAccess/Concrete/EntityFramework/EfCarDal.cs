using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalDatabaseContext>,ICarDal
    {
        public List<CarDetailsDTO> GetCarDetails()
        {
            using (CarRentalDatabaseContext context = new CarRentalDatabaseContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             join co in context.Colors on c.ColorId
                             equals co.Id
                             select new CarDetailsDTO
                             {
                                 BrandName = b.Name,
                                 CarName = c.Name,
                                 ColorName = co.Name,
                                 DailyPrice = c.DailyPrice,
                                Description = c.Description,
                                ModelYear = c.ModelYear,
                             };
                return result.ToList();
            }
        }
    }
}
