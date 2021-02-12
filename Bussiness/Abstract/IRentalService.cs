using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetByID(int id);
        IResult AddRentalCar(Rental rental);
        IResult DeleteRentalCar(Rental rental);
        IResult UpdateRentalCar(Rental rental);
    }
}
