using System.ComponentModel.DataAnnotations;

namespace ECI.Test.Shared.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
    }
}
