namespace BuzzAir.Factories
{
    public static class PersonServiceFactory
    {
        public static Models.DbModels.PassengerService Create(IPassenger person, IService service)
        {
            Passenger passenger = person as Passenger ?? new Passenger();

            Models.DbModels.PassengerService personService = new()
            {
                Passenger = passenger,
                PassengerId = passenger.Id,
                Service = (Service)service,
                ServiceId = service.Id,
            };

            return personService;
        }
    }
}
