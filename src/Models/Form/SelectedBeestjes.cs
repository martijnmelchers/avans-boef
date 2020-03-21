namespace Models.Form
{
    public class SelectedBeestjes
    {
        public int BeestjeId { get; set; }
        public bool Selected { get; set; }

        public SelectedBeestjes(int id)
        {
            BeestjeId = id;
        }
    }
}