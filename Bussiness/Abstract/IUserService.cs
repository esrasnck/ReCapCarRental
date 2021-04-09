using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IUserService
    {
       IDataResult<User> GetByCustomerId(int customerId);
       IDataResult<User> GetByUserId(int userId);
       IDataResult<List<User>> GetAll();
       IResult Delete(User user);
       IResult Update(User user);

       
       IResult AddUser(User user);  
       IDataResult<User> GetByMail(string email); // daha önce bir kullanıcı olup olmadığını tespit etmek için gerekli iş kodu => çözmem gerek :(

        IResult ChangeUserPassword(ChangePasswordDto changePasswordDto);
        IDataResult<List<OperationClaim>> GetClaims(User user);
    }
}
