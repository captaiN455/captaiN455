using System.ComponentModel.DataAnnotations;

namespace AS_AssN.Models
{
    public class Registeration
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }
        //only validate for MasterCard
        [Required, RegularExpression("^5[1-5][0-9]{14}|^(222[1-9]|22[3-9]\\d|2[3-6]\\d{2}|27[0-1]\\d|2720)[0-9]{12}$", ErrorMessage ="This is not Valid Credit Card")]
        public string CreditCard { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required, RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Not Valid Phone Number")]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required, RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\\W)(?!.* ).{5,}$", ErrorMessage = "Password must contain at least one digit, one lowercase, one uppercase, one special character,no space, and a minimum of 5 characters.")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmedPassword { get; set; }
        [MaxLength(100)]
        public string? ImageUrl { get; set; }
        public string? AboutMe { get; set; }


    }
}
