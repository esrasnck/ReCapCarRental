using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
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
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }


        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var IsExist = _userService.GetByMail(userForLoginDto.Email).Data;
            if (IsExist == null) 
            {
                return new ErrorDataResult<User>(Messages.UserNotFound); 
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, IsExist.PasswordHash, IsExist.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError); 
            }
            return new SuccessDataResult<User>(IsExist, Messages.SuccessfullLogin);
        }

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

            _userService.AddUser(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExits(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserExist);
            }
            return new SuccessResult();

        }
    }
}
