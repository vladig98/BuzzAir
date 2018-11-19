namespace BuzzAir.Models
{
    public class SearchModel
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public int PassengersNumber { get; set; }

        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}
