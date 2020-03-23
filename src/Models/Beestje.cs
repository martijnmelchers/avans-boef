using System.Collections.Generic;

namespace Models
{
    public class Beestje
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public double Price { get; set; }

        public string Image { get; set; }


        public IList<BookingBeestje> BookingBeestjes { get; set; }
        public IList<BeestjeAccessoires> BeestjeAccessoires { get; set; }
    }
}
