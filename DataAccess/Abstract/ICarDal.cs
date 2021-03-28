using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
   public  interface ICarDal: IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetail(Expression<Func<CarDetailDto, bool>> filter = null);

        List<CarForDeleteDto> GetCars();
    }
}
