using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IUserService
    {
       IDataResult<User> GetByCustomerId(int customerId);
       IDataResult<User> GetByUserId(int userId);
       IDataResult<User> GetAll();
       IResult Delete(User user);
       IResult Update(User user);
       IResult Add(User user);   // todo: Iresult'a çek
                                  // todo : IDataResult a çek
       IDataResult<User> GetByMail(string email); // daha önce bir kullanıcı olup olmadığını tespit etmek için gerekli iş kodu => çözmem gerek :(
    }
}
