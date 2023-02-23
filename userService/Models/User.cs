using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace userService.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string? Email { get; set; }
        
        public string Username { get; set; }
        public string? UserType { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
