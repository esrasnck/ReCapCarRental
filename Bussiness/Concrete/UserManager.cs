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
            if (_userDal.Any(x=>x.UserId ==user.UserId  || x.Email == user.Email || x.FirstName ==user.FirstName || x.LastName == user.LastName))
            {
                User userToDelete = _userDal.Get(x =>
                    x.UserId == user.UserId || x.Email == user.Email || x.FirstName == user.FirstName ||
                    x.LastName == user.LastName);
                if (userToDelete == null)
                {
                    return new ErrorResult(Messages.UserCantDeleted);
                }
                else
                {
                    _userDal.Delete(userToDelete);
                    return new SuccessResult(Messages.UserDeleted);
                }

                
            }
            return new ErrorResult(Messages.UserNotFound);
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

        public IResult Update(User user)
        {
            if (!_userDal.Any(x=> x.UserId == user.UserId))
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            else
            {
                User userToUpdate = _userDal.Get(x => x.UserId == user.UserId);
                if (userToUpdate != null)
                {
                    if (user.FirstName != null)
                    {
                        userToUpdate.FirstName = user.FirstName.ToUpper();
                    }

                    if (user.LastName!=null)
                    {
                        userToUpdate.LastName = user.LastName.ToUpper();
                    }

                    if (user.Email !=null)
                    {
                        userToUpdate.Email = user.Email.ToLower();
                    }

                    if (user.Status)
                    {
                        userToUpdate.Status = user.Status;
                    }
                    _userDal.Update(userToUpdate);
                    return new SuccessResult(Messages.UserUpdated);
                }
                else
                {
                    return new ErrorResult(Messages.UserCantUpdated);
                }
            }
        }
         
        public IResult AddUser(User user)
        {
            if (user !=null)
            {
                   _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
            }

            return new ErrorResult(Messages.UserCantAdded);

        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(x => x.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

  
    }
}
