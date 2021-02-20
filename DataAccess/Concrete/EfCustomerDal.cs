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
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentACarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerList()
        {
            using (RentACarContext context = new RentACarContext())
            {
                IQueryable<CustomerDetailDto> customerDetails = from u in context.Users
                                                                join c in context.Customers
                                                                on u.UserId equals c.UserId
                                                                select new CustomerDetailDto
                                                                {
                                                                    UserId = u.UserId,
                                                                    CustomerId = c.CustomerId,
                                                                    CustomerName = u.FirstName + " " + u.LastName,
                                                                    CompanyName = c.CompanyName,
                                                                    Email = u.Email,
                                                                    Password = u.Password,
                                                                    Status = u.Status
                                                                };
                return customerDetails.ToList();
            }
        }
    }
}
