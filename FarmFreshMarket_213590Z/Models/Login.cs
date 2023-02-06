using System.ComponentModel.DataAnnotations;

namespace FarmFreshMarket_213590Z.Models
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Token { set; get; }
        public bool RememberMe { get; set; } = false;
    }
}
