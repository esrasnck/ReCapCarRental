using Bussiness.Abstract;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class UserRoleManager:IUserRoleService
    {
        private IUserDal _userDal;

        public UserRoleManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
    }
}
