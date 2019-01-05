using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BuzzAir.Controllers
{
    public class BoardingPassController : Controller
    {
        private AppDbContext db;

        public BoardingPassController(AppDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult BoardingPass(int id)
        {

            var model = new BoardingPassViewModel();
            var booking = this.db.Bookings
                                .Where(x => x.Id == id)
                                .Include(x => x.Flights)
                                    .ThenInclude(x => x.Flight)
                                        .ThenInclude(x => x.Airports)
                                            .ThenInclude(x => x.Airport)
                                .FirstOrDefault();
            model.Departure = booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Departure
                                .ToString("HH:mm")
                    + " " +
                            (booking
                                .Flights
                                .Count() > 1
                            ?
                            booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Airports
                                .Where(x => x.Type == AirportType.Origin)
                                .Select(x => x.Airport.Name)
                                .FirstOrDefault()
                    + " - " +
                            booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Airports
                                .Where(x => x.Type == AirportType.Destination)
                                .Select(x => x.Airport.Name)
                                .FirstOrDefault()
                            :
                                "One Way Ticket")
                    + " " +
                            booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Arrival
                                .ToString("HH:mm")
                    + " " +
                            booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Airports
                                .ToList()[0]
                                .Airport
                                .Terminal
                    + " - " +
                            booking
                                .Flights
                                .ToList()[0]
                                .Flight
                                .Airports
                                .ToList()[1]
                                .Airport
                                .Terminal;
            model.Arrival = booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Departure
                           .ToString("HH:mm")
               + " " +
                       (booking
                           .Flights
                           .Count() > 1
                       ?
                       booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Airports
                           .Where(x => x.Type == AirportType.Origin)
                           .Select(x => x.Airport.Name)
                           .FirstOrDefault()
               + " - " +
                       booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Airports
                           .Where(x => x.Type == AirportType.Destination)
                           .Select(x => x.Airport.Name)
                           .FirstOrDefault()
                       :
                           "One Way Ticket")
               + " " +
                       booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Arrival
                           .ToString("HH:mm")
               + " " +
                       booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Airports
                           .ToList()[0]
                           .Airport
                           .Terminal
               + " - " +
                       booking
                           .Flights
                           .ToList()[1]
                           .Flight
                           .Airports
                           .ToList()[1]
                           .Airport
                           .Terminal;
            return View(model);
        }
    }
}