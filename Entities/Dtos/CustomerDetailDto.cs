using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CustomerDetailDto:IDto
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }

        public int Findeks { get; set; }

    }
}
