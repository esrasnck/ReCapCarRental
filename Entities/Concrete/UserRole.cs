using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class UserRole:IEntity
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }

    }
}
