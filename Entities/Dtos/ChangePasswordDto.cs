using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
   public class ChangePasswordDto:IDto
    {
        public string Email { get; set; }
        public string OldPassWord { get; set; }
        public string NewPassword { get; set; }
    }
}
