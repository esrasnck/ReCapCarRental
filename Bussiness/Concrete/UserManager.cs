using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Bussiness.Constants.Messages;
using Core.Entities.Concrete;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;

namespace Bussiness.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;

        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {

            var result = BusinessRules.Run(CheckIfUserExists(user.UserId));
            if (result != null)
            {
                return new ErrorResult(Messages.UserCantDeleted);
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);

        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UserListed);
        }

        public IDataResult<User> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.UserId == customerId), Messages.UserByCustomerId);
        }

        public IDataResult<User> GetByUserId(int userId)
        {
            return new SuccessDataResult<User>(_userDal.GetByID(userId), Messages.GetByUserId);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            var result = BusinessRules.Run(CheckIfUserExists(user.UserId));
            if (result != null)
            {
                return new ErrorResult(Messages.UserCantDeleted);
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);

        }
        
        [CacheRemoveAspect("IUserService.Get")]
        public IResult AddUser(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        private IResult CheckIfUserExists(int userId)
        {
            var result = _userDal.Any(x => x.UserId == userId);
            if (!result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }

    }
}
