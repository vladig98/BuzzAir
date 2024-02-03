using BuzzAir.Data;
using BuzzAir.Models.DbModels;
using BuzzAir.Services;
using BuzzAir.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Linq.Expressions;
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

                var aircaft = await aircraftService.Create(AircraftName, NumberOfSeats);

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

                Aircraft aircraft = await aircraftService.Create(AircraftName, NumberOfSeats);

                var edited = await aircraftService.Edit(aircraft.Id, AircraftName + "Edited", NumberOfSeats * 2);

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

                Aircraft aircraft = await aircraftService.Create(AircraftName, NumberOfSeats);

                var deleted = await aircraftService.Delete(aircraft.Id);

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

                await aircraftService.Create(AircraftName, NumberOfSeats);
                await aircraftService.Create(AircraftName, NumberOfSeats);
                await aircraftService.Create(AircraftName, NumberOfSeats);

                var count = await aircraftService.GetCount();

                Assert.Equal(3, count);
            }
        }

        [Fact]
        public async Task TestIfAircraftExistsById()
        {
            using (var context = new BuzzAirDbContext(GetOptions()))
            {
                IAircraftService aircraftService = new AircraftService(context);

                Aircraft aircraft = await aircraftService.Create(AircraftName, NumberOfSeats);

                bool exists = await aircraftService.ExistById(aircraft.Id);

                Assert.True(exists);
            }
        }
    }
}
