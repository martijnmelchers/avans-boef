
using System;
using System.Collections.Generic;
using System.Text;
namespace Models
{
    public class BookingAccessoires
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int AccessoireId { get; set; }
        public Accessoire Accessoire { get; set; }
    }
}
