using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;

        }
  
        public IResult Delete(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IResult Update(User user)
        {
            throw new NotImplementedException();
        }

        IResult IUserService.Add(User user)
        {
            throw new NotImplementedException();
        }

        IDataResult<User> IUserService.GetByMail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
