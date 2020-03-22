using System;
using System.Collections.Generic;

namespace Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime Date { get; set; }
        public BookingStep Step { get; set; }



        public Booking()
        {
            AccessToken = Guid.NewGuid().ToString();
        }
        public IList<BookingBeestje> BookingBeestjes { get; set; }
        public IList<BookingAccessoires> BookingAccessoires { get; set; }

    }
}
