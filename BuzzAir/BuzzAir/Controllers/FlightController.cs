using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuzzAir.Controllers
{
    public class FlightController : Controller
    {
        private AppDbContext db;

        public FlightController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult CreateFlight(CreateFlightIndexViewModel model)
        {
            model.Aircrafts = this.db.Aircrafts.ToList();
            model.Airports = this.db.Airports.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateFlightViewModel model)
        {
            var d = string.Empty;
            if (model.Departure.Contains("PM"))
            {
                var s = model.Departure.Split(' ');
                d = s[0] + " " + (s[1].Split(':')[0] == "12" ? "0" : (int.Parse(s[1].Split(':')[0]) + 12).ToString()) + ":" + s[1].Split(":")[1];
            }
            else
            {
                var s = model.Departure.Split(' ');
                d = s[0] + " " + string.Format("{0:D2}", int.Parse(s[1].Split(':')[0])) + ":" + s[1].Split(':')[1];
            }

            var a = string.Empty;
            if (model.Arrival.Contains("PM"))
            {
                var s = model.Arrival.Split(' ');
                a = s[0] + " " + (s[1].Split(':')[0] == "12" ? "0" : (int.Parse(s[1].Split(':')[0]) + 12).ToString()) + ":" + s[1].Split(":")[1];
            }
            else
            {
                var s = model.Arrival.Split(' ');
                a = s[0] + " " + string.Format("{0:D2}", int.Parse(s[1].Split(':')[0])) + ":" + s[1].Split(':')[1];
            }

            var Departure = DateTime.ParseExact(d, "MM/dd/yyyy HH:mm", null);
            var Arrival = DateTime.ParseExact(a, "MM/dd/yyyy HH:mm", null);

            var aircraft = this.db.Aircrafts.Where(x => x.Name == model.Aircraft).FirstOrDefault();

            var origin = this.db.Airports.Where(x => x.Name == model.Origin).FirstOrDefault();
            var destination = this.db.Airports.Where(x => x.Name == model.Destination).FirstOrDefault();

            var flight = new Flight
            {
                FlightNumber = model.FlightNumber,
                Aircraft = aircraft,
                DurationInMinutes = int.Parse(model.DurationInMinutes),
                Departure = Departure,
                Arrival = Arrival,
                Price = decimal.Parse(model.Price)
            };

            var originFlight = new AirportFlight
            {
                Airport = origin,
                Flight = flight,
                Type = AirportType.Origin
            };

            var destinationFlight = new AirportFlight
            {
                Airport = destination,
                Flight = flight,
                Type = AirportType.Destination
            };

            this.db.AirportFlights.Add(originFlight);

            this.db.AirportFlights.Add(destinationFlight);

            flight.Airports.Add(originFlight);
            flight.Airports.Add(destinationFlight);

            this.db.Flights.Add(flight);

            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}