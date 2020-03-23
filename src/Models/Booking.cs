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

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public decimal InitialPrice { get; set; }
        public decimal FinalPrice { get; set; }
        
        public Booking()
        {
            AccessToken = Guid.NewGuid().ToString();
        }

        public IList<Discount> Discounts { get; set; }
        public IList<BookingBeestje> BookingBeestjes { get; set; }
        public IList<BookingAccessoires> BookingAccessoires { get; set; }
    }
}
