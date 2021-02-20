using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
   public interface IAuthService
    {

        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        //IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExits(string email);
    }
}
