using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Discount
    {
        public string Reason { get; set; }
        public int Percentage { get; set; }


        public Discount(string reason, int percentage)
        {
            this.Reason = reason;
            this.Percentage = percentage;
        }
    }
}
