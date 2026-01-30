using System.ComponentModel.DataAnnotations;

namespace ECI.Test.Shared.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Breed { get; set; }

        public int Age { get; set; }
    }
}
