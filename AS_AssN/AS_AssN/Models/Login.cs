using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AS_AssN.Models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
