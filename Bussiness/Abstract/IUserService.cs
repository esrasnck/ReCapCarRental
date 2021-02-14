using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
    public interface IUserService
    {
    

        void Add(User user);   // todo: Iresult'a çek
                                     // todo : IDataResult a çek
        User GetByMail(string email); // daha önce bir kullanıcı olup olmadığını tespit etmek için gerekli iş kodu
    }
}
