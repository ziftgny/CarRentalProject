using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Utilities.Helpers.Concretes;
using Entity.Concrete;
using Entity.DTOs.Request;
using Entity.DTOs.Response;

namespace Business.Utilities.AutoMapper.Profiles
{
    public class CarImageProfile : Profile
    {
        public CarImageProfile()
        {
            CreateMap<CarImage, GetCarImageResponseDTO>();
            CreateMap<AddCarImageRequestDTO, CarImage>().ForSourceMember(src=>src.ImageFile,opt=>opt.DoNotValidate())
                .AfterMap((src, dest) =>
                {
                    dest.Date = DateTime.UtcNow;
                    dest.ImagePath = FileHelper.GenerateFileName(src.ImageFile);
                });
            CreateMap<UpdateCarImageRequestDTO, CarImage>().ForSourceMember(src => src.ImageFile, opt => opt.DoNotValidate())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CarImageId))
                .AfterMap((src, dest) =>
                {
                    dest.Date = DateTime.UtcNow;
                    dest.ImagePath = FileHelper.GenerateFileName(src.ImageFile);
                });
        }
    }
}
