namespace BuzzAir.Models.DbModels.Enums
{
    public enum Currency
    {
        [CustomDisplay(Name = "Euro", Price = "0.91")]
        EUR = 1,
        [CustomDisplay(Name = "Pound Sterling", Price = "0.78")]
        GBP = 2,
        [CustomDisplay(Name = "Bulgarian Lev", Price = "1.78")]
        BGN = 3,
        [CustomDisplay(Name = "United States Dollar", Price = "1")]
        USD = 4,
        [CustomDisplay(Name = "Bosnia-Herzegovina Convertible Marka", Price = "1.79")]
        BAM = 5,
        [CustomDisplay(Name = "Croatian Kuna", Price = "7.03")]
        HRK = 6,
        [CustomDisplay(Name = "Czech Koruna", Price = "22.36")]
        CZK = 7,
        [CustomDisplay(Name = "Georgian Lari", Price = "2.68")]
        GEL = 8,
        [CustomDisplay(Name = "Hungarian Forint", Price = "350.21")]
        HUF = 9,
        [CustomDisplay(Name = "Swiss Franc", Price = "0.86")]
        CHF = 10,
        [CustomDisplay(Name = "Norwegian Krone", Price = "10.27")]
        NOK = 11,
        [CustomDisplay(Name = "Polish Zloty", Price = "3.95")]
        PLN = 12,
        [CustomDisplay(Name = "Russian Ruble", Price = "90.63")]
        RUB = 13,
        [CustomDisplay(Name = "Swedish Krona", Price = "10.16")]
        SEK = 14,
        [CustomDisplay(Name = "Turkish Lira", Price = "29.09")]
        TRY = 15,
        [CustomDisplay(Name = "Ukrainian Hryvnia", Price = "37.32")]
        UAH = 16,
        [CustomDisplay(Name = "Indian Rupee", Price = "83.09")]
        INR = 17,
        [CustomDisplay(Name = "Chinese Yuan", Price = "7.10")]
        CNY = 18,
        [CustomDisplay(Name = "Japanese Yen", Price = "143.80")]
        JPY = 19,
        [CustomDisplay(Name = "Canadian Dollar", Price = "1.33")]
        CAD = 20
    }
}