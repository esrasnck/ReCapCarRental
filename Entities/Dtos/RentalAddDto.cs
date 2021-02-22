using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
   public class RentalAddDto:IDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int? RentalId { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}
