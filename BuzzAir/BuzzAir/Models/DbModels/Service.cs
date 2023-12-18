using BuzzAir.Models.DbModels.Contraccts;
using System.ComponentModel.DataAnnotations;

namespace BuzzAir.Models.DbModels
{
    public abstract class Service : IService
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public abstract decimal Price { get; set; }

        [Required]
        public abstract string Name { get; set; }
    }
}
