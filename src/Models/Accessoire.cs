using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Accessoire
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public string Image { get; set; }

        public IList<BookingAccessoires> BookingAccessoires { get; set; }
        public IList<BeestjeAccessoires> BeestjeAccessoires { get; set; }

    }
}
