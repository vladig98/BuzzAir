using BuzzAir.Data;
using BuzzAir.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BuzzAir.Controllers
{
    public class BookingController : Controller
    {
        private AppDbContext db;

        public BookingController(AppDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public IActionResult Delete (int id)
        {
            var booking = this.db.Bookings.Where(x => x.Id == id).FirstOrDefault();
            booking.Deleted = true;
            this.db.SaveChanges();
            return this.Redirect("/Booking/All");
        }

        [Authorize]
        public IActionResult Details (int id)
        {
            var booking = this.db.Bookings.Where(x => x.Id == id)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Airports)
                            .ThenInclude(x => x.Airport)
                .Include(x => x.Passengers)
                    .ThenInclude(x => x.Person)
                        .ThenInclude(x => x.Services)
                            .ThenInclude(x => x.Service)
                .Include(x => x.Payment)
                .FirstOrDefault();
            var resultFligts = string.Empty;
            for (int i = 0; i < booking.Flights.Count; i++)
            {
                var flight = booking.Flights.ElementAt(i);
                if (i > 0)
                {
                    resultFligts += Environment.NewLine + Environment.NewLine;
                }
                resultFligts += " Number: " + flight.Flight.FlightNumber +
                    Environment.NewLine + " Route: " + flight.Flight.Airports.ElementAt(0).Airport.Name +
                " – " + flight.Flight.Airports.ElementAt(1).Airport.Name +
                Environment.NewLine + " Date: " + flight.Flight.Departure.ToString("dd MMM yyyy") +
                " – " + flight.Flight.Arrival.ToString("dd MMM yyyy") +
                Environment.NewLine + " Time: " + flight.Flight.Departure.ToString("HH:mm") +
                " – " + flight.Flight.Arrival.ToString("HH:mm") +
                Environment.NewLine + " Price: $" + flight.Flight.Price.ToString("F2");
            }
            var resultsPassengers = string.Empty;
            for (int i = 0; i < booking.Passengers.Count; i++)
            {
                var passenegr = booking.Passengers.ElementAt(i).Person;
                var services = passenegr.Services.ToList();
                if (i > 0)
                {
                    resultsPassengers += Environment.NewLine + Environment.NewLine;
                }
                resultsPassengers += " Name: " + passenegr.FullName +
                    Environment.NewLine + " Gender: " + passenegr.Gender +
                    Environment.NewLine + " Services: ";
                for (int j = 0; j < services.Count(); j++)
                {
                    var service = services[j].Service;
                    resultsPassengers += Environment.NewLine + "          - " + (service.Name == "Baggage" ? service.Name + " " + service.Kilos : service.Name == "Seat" ? service.Name + " " + service.Type : service.Name);
                }
            }
            var model = new DetailsViewModel
            {
                Flights = resultFligts,
                Passengers = resultsPassengers,
                Price = booking.Payment.Currency + " " + booking.Payment.Amount.ToString("F2"),
                Id = id
            };
            return this.View(model);
        }

        [Authorize]
        public IActionResult All()
        {
            var bookings = this.db.Bookings
                .Include(x => x.Payment)
                .Include(x => x.Flights)
                    .ThenInclude(x => x.Flight)
                        .ThenInclude(x => x.Airports)
                            .ThenInclude(x => x.Airport)
                .Where(x => x.Deleted == false)
                .ToList();

            var model = new AllBookingsViewModel();

            var userBookings = new List<UserBooking>();
            if (User.IsInRole("Admin"))
            {
                userBookings = this.db.UserBookings
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Booking)
                    .ToList();
            }
            else
            {
                userBookings = this.db.UserBookings
                    .Include(x => x.ApplicationUser)
                    .Include(x => x.Booking)
                    .Where(x => x.ApplicationUser.UserName == User.Identity.Name)
                    .ToList();
            }

            foreach (var booking in bookings)
            {
                if (userBookings.Any(x => x.Booking == booking))
                {
                    model.Bookings.Add(new BookingViewModel
                    {
                        Amount = booking.Payment.Amount.ToString("F2"),
                        Id = booking.Id,
                        Currency = booking.Payment.Currency.ToString(),
                        User = userBookings.Where(x => x.Booking == booking).Select(x => x.ApplicationUser.UserName).FirstOrDefault(),
                        Inbound = booking.Flights.Count() > 1
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
                                    "One Way Ticket",
                        Outbound = booking.Flights.Count() > 0
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
                                "",
                    });
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)
        {
            //–
            var paymentModel = model
                .PaymentsResults
                .Split(' ')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            Enum.TryParse(paymentModel[0].Trim(), out CardType cardType);
            Enum.TryParse(paymentModel[6].Trim(), out Currency currency);

            var holder = string.Empty;
            var indexP = 3;

            while (!Regex.IsMatch(paymentModel[indexP], @"\d+"))
            {
                holder += paymentModel[indexP];
                indexP++;
            }
            var payment = new Payment
            {
                Card = cardType,
                CardNumber = paymentModel[1].Trim(),
                ExpiryDate = DateTime.ParseExact(paymentModel[2].Trim(), "MM/yy", null),
                CardHolder = holder,
                CVC = int.Parse(paymentModel[indexP++]),
                Amount = decimal.Parse(paymentModel[indexP + 1]),
                Currency = currency
            };

            this.db.Payments.Add(payment);
            this.db.SaveChanges();

            var flightsModel = model
                    .FlightsResults
                    .Split(new[]
                    {
                        "Route:", "Date:", "Time:", "Price:", "Number:"
                    },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

            var booking = new Booking
            {
                Payment = payment,
                PaymentId = payment.Id,
            };
            this.db.Bookings.Add(booking);
            this.db.SaveChanges();
            var flightNumbers = new List<string>();
            for (int i = 0; i < flightsModel.Length; i += 5)
            {
                flightNumbers.Add(flightsModel[i]);
            }
            var flights = new List<BookingFlight>();
            var flightsDb = this.db.Flights.ToList();
            foreach (var number in flightNumbers)
            {
                flights.Add(new BookingFlight
                {
                    Flight = flightsDb.Where(x => number == x.FlightNumber).FirstOrDefault(),
                    FlightId = flightsDb.Where(x => number == x.FlightNumber).Select(x => x.Id).FirstOrDefault(),
                    Booking = booking,
                    BookingId = booking.Id
                });
            }
            booking.Flights = flights;
            this.db.SaveChanges();


            var namesModel = model.NamesResults.Split(' ');
            var passengers = GetPassengers(namesModel, model);
            var bPassengers = new List<BookingPassenger>();
            var bFlights = new List<BookingFlight>();
            foreach (var p in passengers)
            {
                bPassengers.Add(new BookingPassenger
                {
                    BookingId = booking.Id,
                    PersonId = p.Id
                });
            }
            booking.Passengers = bPassengers;
            var user = this.db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var userBooking = new UserBooking
            {
                ApplicationUserId = user.Id,
                BookingId = booking.Id,
            };
            this.db.UserBookings.Add(userBooking);
            this.db.SaveChanges();
            return this.Redirect("/");
        }

        public List<Passenger> GetPassengers(string[] namesModel, CreateBookingViewModel model)
        {
            var firstNames = new List<string>();
            var lastNames = new List<string>();
            var dobs = new List<string>();
            var genders = new List<string>();
            var firstName = string.Empty;
            var dob = string.Empty;
            var lastName = string.Empty;
            var gender = string.Empty;
            for (int i = 0; i < namesModel.Length; i++)
            {
                var endIndex = namesModel.ToList().FindIndex(x => x == "–");
                if (i % (endIndex + 2) == 0 && i > 0)
                {
                    firstName = string.Empty;
                    dob = string.Empty;
                    lastName = string.Empty;
                    gender = string.Empty;
                    namesModel[endIndex] = ".";
                }
                var index = namesModel.ToList().FindIndex(x => x == "|");
                if (i < index)
                {
                    firstName += namesModel[i] + " ";
                }
                if (i > index)
                {
                    if (Regex.IsMatch(namesModel[i], @"\d+"))
                    {
                        dob = namesModel[i] + "/" + namesModel[++i] + "/" + namesModel[++i];

                    }
                    else
                    {
                        if (namesModel[i] != "–")
                        {
                            lastName += namesModel[i] + " ";
                        }
                        else
                        {
                            gender = namesModel[++i];
                        }
                    }
                }
                if (i % (endIndex + 1) == 0 && i != 0)
                {
                    firstNames.Add(firstName);
                    lastNames.Add(lastName);
                    genders.Add(gender);
                    dobs.Add(dob);
                    namesModel[index] = ".";
                }
            }
            var docs = model
                .PassportResults
                .Split(new[]
                {
                    "|", "Issue Date:", "Expiry Date:", "Nationality:", "Birth Country:", "Gender:"
                },
                StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            var travelDocs = new List<TravelDocument>();
            for (int i = 0; i < docs.Length; i += 6)
            {
                Enum.TryParse(docs[4].Trim(), out Country birthCountry);
                Enum.TryParse(docs[3].Trim(), out Country nationality);
                Enum.TryParse(docs[5].Trim(), out Gender genderDoc);
                var tokens = docs[0].Trim().Split(new[] { ":", " " }, StringSplitOptions.RemoveEmptyEntries);
                travelDocs.Add(new TravelDocument
                {
                    BirthCountry = birthCountry,
                    ExpiryDate = DateTime.ParseExact(docs[2].Trim(), "d MMMM yyyy", null),
                    Gender = genderDoc,
                    IssueDate = DateTime.ParseExact(docs[1].Trim(), "d MMMM yyyy", null),
                    Nationality = nationality,
                    Number = tokens[1].Trim(),
                    Type = tokens[0].Trim() == "P" ? DocumenType.Passport : DocumenType.National_Id
                });
            }
            this.db.TravelDocuments.AddRange(travelDocs);
            this.db.SaveChanges();
            var services = new List<Service>();
            var serviceModel = model
                .ServicesResults
                .Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
            var passengers = new List<Passenger>();
            for (int i = 0; i < firstNames.Count(); i++)
            {
                Enum.TryParse(genders[i].Trim(), out Gender pGender);
                Random rnd = new Random();
                passengers.Add(new Passenger
                {
                    DateOfBirth = DateTime.ParseExact(dobs[i].Trim(), "d/MMMM/yyyy", null),
                    DocumentId = travelDocs[i].Id,
                    FullName = firstNames[i].Trim() + " " + lastNames[i].Trim(),
                    Gender = pGender,
                    Services = null,
                });
            }
            this.db.Passengers.AddRange(passengers);
            this.db.SaveChanges();
            for (int i = 0; i < serviceModel.Length; i++)
            {
                if (serviceModel[i] == " ")
                {
                    continue;
                }
                services = new List<Service>();
                var tokens = serviceModel[i].Split("–");
                for (int j = 0; j < tokens.Length; j++)
                {
                    if (tokens[j].Trim() == "Airport Check-In")
                    {
                        services.Add(new AirportCheckIn());
                    }
                    else if (tokens[j].Trim() == "Priority")
                    {
                        services.Add(new Priority());
                    }
                    else if (tokens[j].Trim() == "Flexibility")
                    {
                        services.Add(new Flexibility());
                    }
                    else if (tokens[j].Trim() == "On Time Arrival")
                    {
                        services.Add(new OnTimeArrival());
                    }
                    else if (tokens[j].Trim() == "Baggage 32kg")
                    {
                        var baggage = new Baggage();
                        baggage.Kilos = 32;
                        services.Add(baggage);
                    }
                    else if (tokens[j].Trim() == "Baggage 20kg")
                    {
                        var baggage = new Baggage();
                        baggage.Kilos = 20;
                        services.Add(baggage);
                    }
                    else if (tokens[j].Trim() == "Extra Leg-Room Seat")
                    {
                        var seat = new Seat();
                        seat.Type = SeatType.Extra_Leg_Room;
                        services.Add(seat);
                    }
                    else if (tokens[j].Trim() == "Normal Seat")
                    {
                        var seat = new Seat();
                        seat.Type = SeatType.Normal;
                        services.Add(seat);
                    }
                }
                var s = new List<PersonService>();
                this.db.Services.AddRange(services);
                this.db.SaveChanges();
                foreach (var service in services)
                {
                    s.Add(new PersonService
                    {
                        PersonId = passengers[i].Id,
                        ServiceId = service.Id,
                    });
                }
                this.db.PersonServices.AddRange(s);
                passengers[i].Services = s;
                this.db.SaveChanges();
            }
            return passengers;
        }

        public bool CompareDates(DateTime date1, DateTime date2)
        {
            if (date1.Day == date2.Day)
            {
                if (date1.Month == date2.Month)
                {
                    if (date1.Year == date2.Year)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [Authorize]
        public IActionResult CreateBooking(CreateBookingIndexViewModel model)
        {
            if (string.IsNullOrEmpty(model.Origin) || string.IsNullOrEmpty(model.Passenger) ||
                string.IsNullOrEmpty(model.Arrival) || string.IsNullOrEmpty(model.Departure) ||
                string.IsNullOrEmpty(model.Destination))
            {
                return this.Redirect("/");
            }
            var flightsDb = this.db.Flights.Include(x => x.Airports).ThenInclude(x => x.Airport).ToList();
            var flights = flightsDb.Where(x => x.Airports.ElementAt(0).Airport.Name == model.Origin &&
                                               x.Airports.ElementAt(1).Airport.Name == model.Destination &&
                                               CompareDates(x.Departure, DateTime.ParseExact(model.Departure, "MM/dd/yyyy", null)))
                                    .ToList();
            flights.AddRange(flightsDb.Where(x => x.Airports.ElementAt(0).Airport.Name == model.Destination &&
                                               x.Airports.ElementAt(1).Airport.Name == model.Origin &&
                                               CompareDates(x.Arrival, DateTime.ParseExact(model.Arrival, "MM/dd/yyyy", null)))
                                    .ToList());
            foreach (var flight in flights)
            {
                model.FlightsLists.Add(" Number: " + flight.FlightNumber +
                    Environment.NewLine + " Route: " + flight.Airports.ElementAt(0).Airport.Name +
                " – " + flight.Airports.ElementAt(1).Airport.Name +
                Environment.NewLine + " Date: " + flight.Departure.ToString("dd MMM yyyy") +
                " – " + flight.Arrival.ToString("dd MMM yyyy") +
                Environment.NewLine + " Time: " + flight.Departure.ToString("HH:mm") +
                " – " + flight.Arrival.ToString("HH:mm") +
                Environment.NewLine + " Price: $" + flight.Price.ToString("F2"));
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