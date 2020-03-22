using System.ComponentModel.DataAnnotations;

namespace Models.Form
{
    public class ContactInfo
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}