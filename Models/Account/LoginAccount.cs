using System.ComponentModel.DataAnnotations;

namespace TestingBackend.Models
{
    public class LoginAccount
    {
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}