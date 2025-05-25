namespace BuzzAir.Factories
{
    public static class PersonServiceFactory
    {
        public static PersonService Create(IPassenger person, IService service)
        {
            Passenger passenger = person as Passenger ?? new Passenger();

            PersonService personService = new()
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
