namespace Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int Percentage { get; set; }

        public Discount(string reason, int percentage)
        {
            Reason = reason;
            Percentage = percentage;
        }
    }
}
