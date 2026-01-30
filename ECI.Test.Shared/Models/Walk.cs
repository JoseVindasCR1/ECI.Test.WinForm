using System;
using System.ComponentModel.DataAnnotations;

namespace ECI.Test.Shared.Models
{
    public class Walk
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int DogId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public virtual Client Client { get; set; }
        public virtual Dog Dog { get; set; }
    }
}
