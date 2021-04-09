using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetByID(int id);
        IResult AddACustomer(Customer customer);
        IResult DeleteCustomer(Customer customer);
        IResult UpdateCustomer(Customer customer);
        IDataResult<Customer> GetByUserId(int userId);
        IDataResult<List<CustomerDetailDto>> GetCustomerDetail();
        IDataResult<List<CustomerDetailDto>> GetCustomersByCarId(int carId);
    }
}
