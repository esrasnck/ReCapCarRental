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
    public class EfUserDal : EfEntityRepositoryBase<User, RentACarContext>, IUserDal
    { 
        // todo: buraya dön => oldu sanki :)
        public List<Customer> GetCustomers(User user) // joinledim :(
        {
            using (RentACarContext context= new RentACarContext())
            {
                IQueryable<Customer> result = from customer in context.Customers
                                              join userRole in context.UserRoles
                                              on customer.CustomerId equals userRole.CustomerId
                                              where userRole.UserId == user.UserId
                                              select new Customer
                                              {
                                                  CustomerId = customer.CustomerId,
                                                  CompanyName = customer.CompanyName
                                              };
                return result.ToList();
            }
        }

   
    }
}
