namespace BuzzAir.Utilities
{
    public sealed class CustomDisplayAttribute : Attribute
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string IconURL { get; set; }
    }
}
