using System.Reflection;

namespace BuzzAir.Utilities
{
    public static class GetDisplayNameExtensionMethod
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<CustomDisplayAttribute>()
                        ?.Name;
        }

        public static string GetPrice(this Enum enumValue)
        {
            return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<CustomDisplayAttribute>()
                        ?.Price;
        }

        public static string GetIconURL(this Enum enumValue)
        {
            return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<CustomDisplayAttribute>()
                        ?.IconURL;
        }
    }
}
