using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        //public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        //{
        //    var IsExist = _userService.GetByMail(userForLoginDto.Email);
        //    if (IsExist == null) //=> kullanıcı var mı?
        //    {
        //        return new ErrorDataResult<User>(Messages.UserNotFound); // => şifre doğru mu?
        //    }
        //    if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, IsExist.PasswordHash, IsExist.PasswordSalt))
        //    {
        //        return new ErrorDataResult<User>(Messages.PasswordError); // api ya gerçek sonucu vermeliyiz.
        //    }
        //    return new SuccessDataResult<User>(IsExist, Messages.SuccessfullLogin);
        //}

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User();
            user.Email = userForRegisterDto.Email;
            user.FirstName = userForRegisterDto.FirstName;
            user.LastName = userForRegisterDto.LastName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Status = true;

            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExits(string email)
        {
            if (_userService.GetByMail(email)!=null)
            {
                return new ErrorResult(Messages.UserExist);
            }
            return new SuccessResult();

        }
    }
}
