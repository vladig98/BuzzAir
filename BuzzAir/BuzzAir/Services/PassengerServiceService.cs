namespace BuzzAir.Services
{
    public class PassengerServiceService(BuzzAirDbContext context) : IPassengerServiceService
    {
        public async Task<Models.DbModels.PassengerService> Create(IPassenger passenger, IService service)
        {
            Models.DbModels.PassengerService paxSevice = PersonServiceFactory.Create(passenger, service);

            await context.PersonServices.AddAsync(paxSevice);
            await context.SaveChangesAsync();

            return paxSevice;
        }

        public async Task CreateAsync(IPassenger passenger, List<IService> services)
        {
            List<Task<Models.DbModels.PassengerService>> personServiceTasks = [];

            foreach (IService service in services)
            {
                Task<Models.DbModels.PassengerService> personServiceTask = Create(passenger, service);

                personServiceTasks.Add(personServiceTask);
            }

            await Task.WhenAll(personServiceTasks);

            foreach (Task<Models.DbModels.PassengerService> completedTask in personServiceTasks)
            {
                passenger.Services.Add(completedTask.Result);
            }

            await context.SaveChangesAsync();
        }
    }
}
