using Core.DataAccess.EntityFramework;
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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerList(Expression<Func<CustomerDetailDto, bool>> filter=null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                IQueryable<CustomerDetailDto> customerDetails = from u in context.Users
                                                                join c in context.Customers
                                                                on u.UserId equals c.UserId
                                                                join r in context.Rentals
                                                                on c.CustomerId equals r.CustomerId
                                                                join cr in context.Cars
                                                                on r.CarId equals cr.CarId
                                                                select new CustomerDetailDto
                                                                {
                                                                    UserId = u.UserId,
                                                                    CustomerId = c.CustomerId,
                                                                    CarId = cr.CarId,
                                                                    CustomerName = u.FirstName + " " + u.LastName,
                                                                    CompanyName = c.CompanyName,
                                                                    Findeks = c.Findeks
                                                                   
                                                                };
                return filter == null ? customerDetails.ToList() : customerDetails.Where(filter).ToList();
            }
        }
    }
}
