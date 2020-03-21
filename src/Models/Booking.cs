using System;
using System.Collections.Generic;

namespace Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public DateTime Date { get; set; }


        public Booking()
        {
            AccessToken = Guid.NewGuid().ToString();
        }
    }
}
