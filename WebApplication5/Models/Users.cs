using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
