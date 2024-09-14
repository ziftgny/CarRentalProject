using Core.DataAccess.EntityFramework;
using Core.Entities.Concretes;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User,CarRentalDatabaseContext>,IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context =new CarRentalDatabaseContext())
            {
                var result = from userOperationClaims in context.UserOperationClaims
                             join operationClaims in context.OperationClaims on
                             userOperationClaims.OperationClaimId equals operationClaims.Id
                             where userOperationClaims.UserId == user.Id
                             select new OperationClaim { Id = operationClaims.Id,
                             Name = operationClaims.Name
                             };
                return result.ToList();
            }
        }
    }
}
