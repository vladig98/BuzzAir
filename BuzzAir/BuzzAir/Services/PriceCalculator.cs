namespace BuzzAir.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal Calculate(CreateBookingViewModel model, Flight outbound, Flight? inbound)
        {
            decimal price = 0;
            decimal passengerServices = 0;
            decimal baggagePrice = 0;

            foreach (PassengerViewModel passenger in model.Passengers)
            {
                foreach (ServiceViewModel service in passenger.Services)
                {
                    if (!service.IsChecked)
                    {
                        continue;
                    }

                    // Baggage was purchased
                    if (service.Kilos == 20)
                    {
                        baggagePrice += GlobalConstants.PriceFor20kg;
                    }
                    else if (service.Kilos == 32)
                    {
                        baggagePrice += GlobalConstants.PriceFor32kg;
                    }

                    passengerServices += service.Price;
                }
            }

            int passengersCount = model.Passengers.Count;
            decimal outboundPrice = outbound.Price * passengersCount;
            decimal inboundPrice = (inbound?.Price ?? 0) * passengersCount;

            price += passengerServices + outboundPrice + inboundPrice + baggagePrice;

            return price;
        }
    }
}
