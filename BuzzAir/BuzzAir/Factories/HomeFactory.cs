
namespace BuzzAir.Factories
{
    public static class HomeFactory
    {
        internal static IndexViewModel CreateViewModel(List<SelectListItem> origins)
        {
            IndexViewModel model = new()
            {
                Origins = origins.OrderBy(x => x.Group.Name).ThenBy(x => x.Text)
            };

            return model;
        }
    }
}
