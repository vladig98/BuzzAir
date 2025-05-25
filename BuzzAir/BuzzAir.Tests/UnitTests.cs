using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BuzzAir.Tests
{
    public class UnitTests
    {
        //Aircraft Tests

        private const string AircraftName = "Airbus123";
        private const int NumberOfSeats = 200;

        private DbContextOptions<BuzzAirDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<BuzzAirDbContext>().UseInMemoryDatabase(databaseName: "UnitTestDB").Options;
        }

        [Fact]
        public async Task TestIfWeCanCreateAnAircraft()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                var aircaft = await aircraftService.CreateAsync(AircraftName, NumberOfSeats);

                Assert.Equal(AircraftName, aircaft.Name);
                Assert.Equal(NumberOfSeats, aircaft.NumberOfSeats);
                Assert.False(aircaft.IsDeleted);
            }
        }

        [Fact]
        public async Task TestIfWeCanEditAnAircraft()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                Aircraft aircraft = await aircraftService.CreateAsync(AircraftName, NumberOfSeats);

                var edited = await aircraftService.EditAsync(aircraft.Id, AircraftName + "Edited", NumberOfSeats * 2);

                Assert.Equal(AircraftName + "Edited", edited.Name);
                Assert.Equal(NumberOfSeats * 2, edited.NumberOfSeats);
                Assert.Equal(aircraft.Id, edited.Id);
                Assert.False(edited.IsDeleted);
            }
        }

        [Fact]
        public async Task TestIfWeCanDeleteAnAircraft()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                Aircraft aircraft = await aircraftService.CreateAsync(AircraftName, NumberOfSeats);

                var deleted = await aircraftService.DeleteAsync(aircraft.Id);

                Assert.Equal(AircraftName, deleted.Name);
                Assert.Equal(NumberOfSeats, deleted.NumberOfSeats);
                Assert.Equal(aircraft.Id, deleted.Id);
                Assert.True(deleted.IsDeleted);
            }
        }

        [Fact]
        public async Task TestIfGetCountReturnsTheCorrectValue()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                await aircraftService.CreateAsync(AircraftName, NumberOfSeats);
                await aircraftService.CreateAsync(AircraftName, NumberOfSeats);
                await aircraftService.CreateAsync(AircraftName, NumberOfSeats);

                var count = await aircraftService.GetCountAsync();

                Assert.Equal(3, count);
            }
        }

        [Fact]
        public async Task TestIfAircraftExistsById()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                Aircraft aircraft = await aircraftService.CreateAsync(AircraftName, NumberOfSeats);

                bool exists = await aircraftService.ExistByIdAsync(aircraft.Id);

                Assert.True(exists);
            }
        }
    }
}
