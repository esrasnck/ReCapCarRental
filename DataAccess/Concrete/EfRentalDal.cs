using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                IQueryable<RentalDetailDto> rentalDetails = from r in filter is null ? context.Rentals:context.Rentals.Where(filter)
                                    join c in context.Cars
                                    on r.CarId equals c.CarId
                                    join cs in context.Customers
                                    on r.CustomerId equals cs.CustomerId
                                    join user in context.Users
                                    on cs.UserId equals user.UserId
                                    select new RentalDetailDto
                                    {
                                        CarId = c.CarId,
                                        RentalId = r.RentalId,
                                        CarName = c.CarName,
                                        CompanyName = cs.CompanyName,
                                        UserName = user.FirstName + " " + user.LastName,
                                        RentDate = r.RentDate,
                                        ReturnDate = r.ReturnDate
                                    };
                return rentalDetails.ToList();
            }
        }

        public List<RentalAddDto> RentalAdd()
        {
            using(RentACarContext context= new RentACarContext())
            {
                IQueryable<RentalAddDto> result = from c in context.Cars
                             join r in context.Rentals
                             on c.CarId equals r.CarId
                             join cs in context.Customers
                             on r.CustomerId equals cs.CustomerId
                             select new RentalAddDto
                             {
                                 CarId = c.CarId,
                                 RentalId = r.RentalId,
                                 CustomerId = cs.CustomerId,
                                 ReturnDate = r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }

  
}
 