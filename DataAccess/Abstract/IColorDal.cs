using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using System;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IColorDal : IEntityRepository<Color>
    {

    }
}
