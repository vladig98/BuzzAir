using BuzzAir.Models.DbModels.Contraccts;

namespace BuzzAir.Services
{
    public class PassengerServiceService(BuzzAirDbContext context) : IPassengerServiceService
    {
        public async Task<PersonService> Create(IPassenger passenger, IService service)
        {
            PersonService paxSevice = PersonServiceFactory.Create(passenger, service);

            await context.PersonServices.AddAsync(paxSevice);
            await context.SaveChangesAsync();

            return paxSevice;
        }

        public async Task CreateAsync(IPassenger passenger, List<IService> services)
        {
            List<Task<PersonService>> personServiceTasks = [];

            foreach (IService service in services)
            {
                Task<PersonService> personServiceTask = Create(passenger, service);

                personServiceTasks.Add(personServiceTask);
            }

            await Task.WhenAll(personServiceTasks);

            foreach (Task<PersonService> completedTask in personServiceTasks)
            {
                passenger.Services.Add(completedTask.Result);
            }

            await context.SaveChangesAsync();
        }
    }
}
