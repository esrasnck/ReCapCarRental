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
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarForDeleteDto> GetCars()
        {
            using (RentACarContext context = new RentACarContext())
            {
                IQueryable<CarForDeleteDto> carForDelete = from c in context.Cars
                                                           join r in context.Rentals
                                                           on c.CarId equals r.CarId
                                                           select new CarForDeleteDto
                                                           {
                                                               CarId = c.CarId,
                                                               RentalId = r.RentalId,
                                                               RentedDate = r.RentDate,
                                                               ReturnedDate = r.ReturnDate
                                                           };
                return carForDelete.ToList();

            }
        }

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
                                                          Id = c.CarId,
                                                          BrandId = b.BrandId,
                                                          ColorId = cl.ColorId,
                                                          CarName = c.CarName,
                                                          BrandName = b.BrandName,
                                                          ColorName = cl.ColorName,
                                                          ModelYear = c.ModelYear,
                                                          DailyPrice = c.DailyPrice.ToString(),
                                                          Description = c.Description
                                                      };

                return carDetails.ToList();
            }
        }
    }
}
