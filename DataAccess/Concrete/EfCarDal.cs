using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfCarDal :  EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {

        public List<CarDetailDto> GetCarDetail()
        {
            using (RentACarContext context = new RentACarContext())
            {
                IQueryable<CarDetailDto> carDetails = from c in context.Cars
                                                      join b in context.Brands
                                                      on c.BrandId equals b.BrandId
                                                      join cl in context.Colors
                                                      on c.ColorId equals cl.ColorId
                                                      select new CarDetailDto
                                                      {
                                                          CarName = c.CarName,
                                                          BrandName = b.BrandName,
                                                          ColorName = cl.ColorName,
                                                          DailyPrice = c.DailyPrice.ToString()
                                                      };

                return carDetails.ToList();
            }
        }
    }
}
