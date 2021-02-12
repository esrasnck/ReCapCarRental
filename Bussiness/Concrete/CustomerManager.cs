using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;


namespace Bussiness.Concrete
{
    public class CustomerManager:ICustomerService
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
        public IResult AddACustomer(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult DeleteCustomer(Customer customer)
        {
            Customer customerFind = _customerDal.GetByID(customer.CustomerId);
            if (customerFind == null)
            {
                return new ErrorResult(Messages.CustomerNotDeleted);
            }
            else
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.CustomerDeleted);
            }
        }

        public IResult UpdateCustomer(Customer customer)
        {
            Customer customerFind = _customerDal.GetByID(customer.CustomerId);
            if (customerFind == null)
            {
                return new ErrorResult(Messages.CustomerNotUpdated);
            }
            else
            {
                _customerDal.Update(customer);
                return new SuccessResult(Messages.CustomerUpdated);
            }
        }

    }
}
