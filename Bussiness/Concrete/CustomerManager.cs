using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

      //  [CacheRemoveAspect("ICustomerService.Get")]
        public IResult AddACustomer(Customer customer)
        {
            if (!_customerDal.Any(x => x.UserId == customer.UserId))
            {
                return new ErrorResult(Messages.CustomerNotAdded);
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }


      //  [CacheRemoveAspect("ICustomerService.Get")]
        public IResult DeleteCustomer(Customer customer)
        {
            if (_customerDal.Any(x => x.CustomerId == customer.CustomerId || x.UserId == customer.UserId || x.CompanyName == customer.CompanyName))
            {

                Customer customerFind = _customerDal.GetByID(customer.CustomerId);
                if (customerFind == null)
                {
                    return new ErrorResult(Messages.CustomerNotDeleted);
                }
                else
                {
                    _customerDal.Delete(customerFind);
                    return new SuccessResult(Messages.CustomerDeleted);
                }
            }
            return new ErrorResult(Messages.CustomerNotDeleted);
        }


      //  [CacheRemoveAspect("ICustomerService.Get")]
        public IResult UpdateCustomer(Customer customer)
        {

            if (!_customerDal.Any(x => x.CustomerId == customer.CustomerId))
            {
                return new ErrorResult(Messages.CustomerNotUpdated);
            }
            else
            {
                Customer customerFind = _customerDal.Get(x => x.CustomerId == customer.CustomerId);

                if (customerFind != null)
                {
                    if (customer.CompanyName != null)
                    {
                        customerFind.CompanyName = customer.CompanyName;

                    }
                    if (customer.UserId>0 && _customerDal.Any(x => x.UserId == customer.UserId))
                    {
                        customerFind.UserId = customer.UserId;
                    }
                    _customerDal.Update(customerFind);
                    return new SuccessResult(Messages.CustomerUpdated);

                }
                else
                {

                    return new ErrorResult(Messages.CustomerNotUpdated);

                }

            }
        }

    }
}
