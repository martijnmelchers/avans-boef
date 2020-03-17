using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Form
{
    public class DateSelection
    {
        [DisplayName("Kies een datum")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BookingDate { get; set; }
    }
}