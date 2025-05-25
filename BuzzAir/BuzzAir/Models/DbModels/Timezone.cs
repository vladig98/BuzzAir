namespace BuzzAir.Models.DbModels
{
    public class Timezone
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
