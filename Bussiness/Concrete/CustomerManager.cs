using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;


namespace Bussiness.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomerListed);
        }

        public IDataResult<Customer> GetByID(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.CustomerId == id), Messages.CustomerById);
        }

        [CacheRemoveAspect("ICustomerService.Get")]

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult AddACustomer(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }


        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult DeleteCustomer(Customer customer)
        {
            var result = BusinessRules.Run(CheckIfCustomerExits(customer.CustomerId));
            if (result != null)
            {
                return new ErrorResult(Messages.CustomerNotDeleted);
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleted);

        }


        [CacheRemoveAspect("ICustomerService.Get")]

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult UpdateCustomer(Customer customer)
        {
            var result = BusinessRules.Run(CheckIfCustomerExits(customer.CustomerId));
            if (result != null)
            {
                return new ErrorResult(Messages.CustomerNotDeleted);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerDeleted);

        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetail()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerList(), Messages.CustomerDetailListed);

        }

        public IDataResult<List<CustomerDetailDto>> GetCustomersByCarId(int carId)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerList(x => x.CarId == carId), Messages.CustomerListed);
        }

        private IResult CheckIfCustomerExits(int customerId)  
        {
            var result = _customerDal.Any(x => x.CustomerId == customerId);
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }


    }
}
