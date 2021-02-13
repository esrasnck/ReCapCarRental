using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentACarContext>, IUserDal
    { 
    }
}
