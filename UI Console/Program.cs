using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;


UserManager userManager = new UserManager(new EfUserDal());
foreach (var item in userManager.GetAll().Data)
{
    Console.WriteLine(item.Id+" - "+item.Email+" - "+item.FirstName+" - "+item.Password);
}


