﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
   public class Rental:IEntity
    {
        public Rental()
        {
            RentDate = DateTime.Now;
        }
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool StilRented { get; set; } // todo: şunu bir düşün !!!

    }
}
