namespace BuzzAir.Controllers
{
    public class BookingController(
        IBookingService bookingService) : Controller
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await bookingService.DeleteAsync(id);

            return this.Redirect("/Booking/All");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            BookingViewModel viewModel = await bookingService.GetBookingDetailsAsync(id);

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(BookingViewModel editViewModel)
        {
            // TO DO: Implement
            // TO DO: Make a booking edit view model
            throw new NotImplementedException();
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            AllBookingsViewModel viewModel = await bookingService.GetAllAsync(User.Identity?.Name);

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateBooking", model);
            }

            await bookingService.CreateAsync(model, User.Identity?.Name ?? string.Empty);

            return Redirect("All");
        }

        /// <summary>
        ///  This acts as a Get request for the Create Booking method. 
        ///  It's a post request cause we need some data before we can start the booking process.
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("/", model);
            }

            CreateBookingViewModel viewModel = await bookingService.CreateViewModelAsync(model);

            return View(viewModel);
        }
    }
}