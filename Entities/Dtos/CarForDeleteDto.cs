using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CarForDeleteDto:IDto
    {
        public int CarId { get; set; }
        public int RentalId { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

    }
}
