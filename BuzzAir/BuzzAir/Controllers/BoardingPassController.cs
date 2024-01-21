using BuzzAir.Models.DbModels;
using BuzzAir.Models.ViewModels;
using BuzzAir.Services.Contracts;
using BuzzAir.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class BoardingPassController : Controller
    {
        private readonly IBookingService _bookingService;

        public BoardingPassController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize]
        public async Task<IActionResult> BoardingPass(string id)
        {
            BoardingPassViewModel model = new BoardingPassViewModel();

            Booking booking = await _bookingService.GetById(id);

            if (booking == null)
            {
                return BadRequest();
            }

            model.Departure = string.Format(GlobalConstants.BoardingPassFormat,
                                            booking.Flights.First(x => x.IsOutbound).Flight.Departure.ToString(GlobalConstants.TimeFormat),
                                            booking.Flights.First(x => x.IsOutbound).Flight.Origin.Name,
                                            booking.Flights.First(x => x.IsOutbound).Flight.Destination.Name,
                                            booking.Flights.First(x => x.IsOutbound).Flight.Arrival.ToString(GlobalConstants.TimeFormat),
                                            booking.Flights.First(x => x.IsOutbound).Flight.Origin.IATA,
                                            booking.Flights.First(x => x.IsOutbound).Flight.Destination.IATA);

            model.Arrival = booking.Flights.Count() > 1 ? string.Format(GlobalConstants.BoardingPassFormat,
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Departure.ToString(GlobalConstants.TimeFormat),
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Origin.Name,
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Destination.Name,
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Arrival.ToString(GlobalConstants.TimeFormat),
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Origin.IATA,
                                           booking.Flights.First(x => !x.IsOutbound).Flight.Destination.IATA) : GlobalConstants.OneWayTicket;
            return View(model);
        }
    }
}