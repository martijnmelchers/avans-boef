namespace Models
{
    public class BookingBeestje
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int BeestjeId { get; set; }
        public Beestje Beestje { get; set; }
    }
}

