﻿using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetRentalById(int id);
        IDataResult<List<Rental>> GetAll();
        IResult Add(Rental rental);
        IResult Remove(Rental rental);
        IResult Update(Rental rental);
    }
}
