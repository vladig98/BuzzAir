namespace BuzzAir.Factories
{
    public static class BoardingPassFactory
    {
        public static BoardingPassViewModel CreateViewModel(string departure, string arrival)
        {
            BoardingPassViewModel viewModel = new()
            {
                Departure = departure,
                Arrival = arrival
            };

            return viewModel;
        }
    }
}
