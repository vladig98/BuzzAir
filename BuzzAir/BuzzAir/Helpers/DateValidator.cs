namespace BuzzAir.Helpers
{
    public static class DateValidator
    {
        public static bool IsValidExpiryDate(string date, out DateTime parsedDate)
        {
            bool validDate = DateTime.TryParseExact(date, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            return validDate;
        }
    }
}
