
using System;
using System.Collections.Generic;
using System.Text;
namespace Models
{
    public class BeestjeAccessoires
    {
        public int BeestjeId { get; set; }
        public Beestje Beestje{ get; set; }

        public int AccessoireId { get; set; }
        public Accessoire Accessoire { get; set; }
    }
}
