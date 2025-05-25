namespace BuzzAir.Models.DbModels.Enums
{
    public enum BaggageType
    {
        [CustomDisplay(Price = "Free", Name = "Cabin", IconURL = "<i class=\"fa-solid fa-bag-shopping\"></i>")]
        Cabin = 0,
        [CustomDisplay(Name = "20kg", Price = GlobalConstants.PriceFor20kgBag, IconURL = "<i class=\"fa-solid fa-suitcase\"></i>")]
        Twenty_KG = 1,
        [CustomDisplay(Name = "32kg", Price = GlobalConstants.PriceFor32kgBag, IconURL = "<i class=\"fa-solid fa-suitcase-rolling\"></i>")]
        ThirtyTwo_KG = 2
    }
}
