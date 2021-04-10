﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public List<CarDetailDto> GetCarDetail(Expression<Func<Car, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var carDetails = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                                 join b in context.Brands
                                                      on c.BrandId equals b.BrandId
                                                      join cl in context.Colors
                                                      on c.ColorId equals cl.ColorId
                                                      select new CarDetailDto
                                                      {
                                                          CarId = c.CarId,
                                                          BrandId = b.BrandId,
                                                          ColorId = cl.ColorId,
                                                          CarName = c.CarName,
                                                          Findeks = c.Findeks,
                                                          BrandName = b.BrandName,
                                                          ColorName = cl.ColorName,
                                                          ModelYear = c.ModelYear,
                                                          DailyPrice = c.DailyPrice,
                                                          Description = c.Description,
                                                  
                                                          ImagePath = (from m in context.CarImages where m.CarId == c.CarId select m.ImagePath).FirstOrDefault(),
                                                          IsRentable = !context.Rentals.Any(r => r.CarId == c.CarId) || !context.Rentals.Any(r => r.CarId == c.CarId && (r.ReturnDate == null || (r.ReturnDate.HasValue && r.ReturnDate > DateTime.Now)))
                                                      };


                return carDetails.ToList();

           
        }
    }
}
}
