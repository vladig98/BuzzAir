namespace BuzzAir.Areas.Admin.Controllers;

[Area(GlobalConstants.AdminRole)]
public class CityController(
    ICountryService countryService,
    ITimezoneService timezoneService,
    ICityService cityService,
    IValidationService validationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken token)
    {
        List<CountryDTO> countries = await countryService.GetAllCountriesAsync(token);
        List<TimezoneDTO> timezones = await timezoneService.GetTimezonesAsync(token);

        SelectListGroup countryGroup = new() { Name = "Officially recognized countries" };
        SelectListGroup dependencyGroup = new() { Name = "Territories not officially recognized as coutnries" };

        CreateCityVM model = new();

        foreach (CountryDTO country in countries)
        {
            model.Countries.Add(new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id,
                Group = country.IsOfficiallyRecognizedCountry ? countryGroup : dependencyGroup
            });
        }

        foreach (TimezoneDTO timezone in timezones)
        {
            model.Timezones.Add(new SelectListItem()
            {
                Text = timezone.Name,
                Value = timezone.Id
            });
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCityVM viewModel, CancellationToken token)
    {
        await validationService.ValidateAsync(viewModel, ModelState, token);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await cityService.AddCityAsync(viewModel, token);

        return Redirect("/");
    }
}