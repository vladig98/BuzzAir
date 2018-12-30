using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuzzAir.Controllers
{
    public class BookingController : Controller
    {
        private AppDbContext db;

        public BookingController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Create(CreateBookingIndexViewModel model)
        {
            var names = model.FirstNames;
            return this.Redirect("/");
        }

        public IActionResult CreateBooking(CreateBookingIndexViewModel model)
        {
            if (string.IsNullOrEmpty(model.Origin) || string.IsNullOrEmpty(model.Passenger) ||
                string.IsNullOrEmpty(model.Arrival) || string.IsNullOrEmpty(model.Departure) ||
                string.IsNullOrEmpty(model.Destination))
            {
                return this.Redirect("/");
            }
            var origin = this.db.Airports.Where(x => x.Name == model.Origin).FirstOrDefault();
            var flights = new List<Flight>();
            for (int i = 0; i < this.db.Flights.ToList().Count(); i++)
            {
                var flight = this.db.Flights.ToList()[i];
                var airports = this.db.AirportFlights.Where(x => x.FlightId == flight.Id).ToList();
                if (airports.Any(x => (x.Type == AirportType.Origin && x.AirportId == origin.Id)))
                {
                    flights.Add(flight);
                }
            }
            var destination = this.db.Airports.Where(x => x.Name == model.Destination).FirstOrDefault();
            for (int i = flights.Count() - 1; i >= 0; i--)
            {
                var flight = flights[i];
                var airports = flight.Airports.Where(x => x.FlightId == flight.Id).ToList();
                if (!airports.Any(x => x.Type == AirportType.Destination && x.AirportId == destination.Id))
                {
                    flights.Remove(flights[i]);
                }
            }
            //–
            foreach (var flight in flights)
            {
                var o = this.db.Airports.Where(x => x.Id == flight.Airports.Where(y => y.Type == AirportType.Origin).FirstOrDefault().AirportId).FirstOrDefault();
                var d = this.db.Airports.Where(x => x.Id == flight.Airports.Where(y => y.Type == AirportType.Destination).FirstOrDefault().AirportId).FirstOrDefault();
                model.FlightsLists.Add("Route: " + o.Name +
                " – " + d.Name +
                Environment.NewLine + "Date: " + flight.Departure.ToString("dd MMM yyyy") +
                " – " + flight.Arrival.ToString("dd MMM yyyy") +
                Environment.NewLine + "Time: " + flight.Departure.ToString("HH:mm") +
                " – " + flight.Arrival.ToString("HH:mm") +
                Environment.NewLine + "Price: $" + flight.Price.ToString("F2"));
            }
            model.Flights = flights;
            model.AirportCheckInPrice = new AirportCheckIn().Price.ToString();
            var baggage = new Baggage();
            baggage.Kilos = 20;
            model.Baggage20kgPrice = baggage.Price.ToString();
            baggage.Kilos = 32;
            model.Baggage32kgPrice = baggage.Price.ToString();
            var seat = new Seat();
            seat.Type = SeatType.Normal;
            model.ExtraLegRoomSeatPrice = seat.Price.ToString();
            model.FlexibilityPrice = new Flexibility().Price.ToString();
            seat.Type = SeatType.Extra_Leg_Room;
            model.NormalSeatPrice = seat.Price.ToString();
            model.OnTimeArrivalPrice = new OnTimeArrival().Price.ToString();
            model.PriorityPrice = new Priority().Price.ToString();
            return View(model);
        }
    }
}