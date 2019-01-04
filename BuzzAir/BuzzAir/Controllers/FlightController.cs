using System;
using System.Collections.Generic;
using System.Linq;
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

            var airports = this.db.Airports.Where(x => x.Name == model.Origin || x.Name == model.Destination).ToList();


            var flight = new Flight
            {
                Aircraft = this.db.Aircrafts.Where(x => x.Name == model.Aircraft).FirstOrDefault(),
                AircraftId = this.db.Aircrafts.Where(x => x.Name == model.Aircraft).Select(x => x.Id).FirstOrDefault(),
                DurationInMinutes = int.Parse(model.DurationInMinutes),
                Price = decimal.Parse(model.Price),
                Departure = Departure,
                Arrival = Arrival,
                FlightNumber = model.FlightNumber,
                Airports = new List<AirportFlight>
                {
                    new AirportFlight
                    {
                        Airport = airports[0],
                        AirportId = airports[0].Id,
                        Type = AirportType.Origin
                    },
                    new AirportFlight
                    {
                        Airport = airports[1],
                        AirportId = airports[1].Id,
                        Type = AirportType.Destination
                    }
                }
            };

            this.db.Flights.Add(flight);
            this.db.SaveChanges();

            var flightId = flight.Id;
            var flightTemp = flight;

            for (int i = 0; i < flight.Airports.Count(); i++)
            {
                flight.Airports.ToList()[i].FlightId = flightId;
                flight.Airports.ToList()[i].Flight = flightTemp;
            }

            this.db.SaveChanges();
            return this.Redirect("/");
        }
    }
}