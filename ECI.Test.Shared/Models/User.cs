using System;
using System.ComponentModel.DataAnnotations;

namespace ECI.Test.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public User()
        {
            CreateDate = DateTime.Now;
        }
    }
}