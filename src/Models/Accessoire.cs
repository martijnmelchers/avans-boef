using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Accessoire
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }

        public IList<BookingAccessoires> BookingAccessoires { get; set; }
        public IList<BeestjeAccessoires> BeestjeAccessoires { get; set; }

    }
}
