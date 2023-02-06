using FarmFreshMarket_213590Z.Models;
using FarmFreshMarket_213590Z.Models;
using FarmFreshMarket_213590Z.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Web.Lib;

namespace FarmFreshMarket_213590Z.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<AuthUser> userManager { get; }
        private SignInManager<AuthUser> signInManager { get; }

        private IWebHostEnvironment _environment;
        private RoleManager<IdentityRole> roleManager { get; }

        private readonly IDataProtector _protector;
        private readonly IHttpContextAccessor _contxt;

        [BindProperty]
        public AuthUser RModel { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$", ErrorMessage = "Use a minimum of 12 characters, including lower-case and upper-case , numbers and special characters")]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public string Register { get; set; }

        public RegisterModel(IHttpContextAccessor httpContextAccessor, 
            IDataProtectionProvider provider, 
            UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager, IWebHostEnvironment environment, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
            _contxt = httpContextAccessor;
            _protector = provider.CreateProtector("credit_card_protector");
            this.roleManager = roleManager;
            //_resetPasswordService = resetPasswordService;

        }

        public void OnGet()
        {



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            //upload image
            if (ModelState.IsValid)
            {
                //for all other data
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptDara");
                

                if (Upload != null)
                {
                    if (Upload.Length > 2097152)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    var ImageURL = string.Format("/" + uploadsFolder + "/" + imageFile);

                    //protect data
                    var protectedCreditCardNumber = _protector.Protect(RModel.CreditCardNumber);
                    var protectedUserName = _protector.Protect(System.Web.HttpUtility.HtmlEncode(EmailAddress));
                    var protectedPassword = _protector.Protect(System.Web.HttpUtility.HtmlEncode(Password));
                    var protectedFullName = _protector.Protect(System.Web.HttpUtility.HtmlEncode(RModel.FullName));
                    var protectedGender = _protector.Protect(System.Web.HttpUtility.HtmlEncode(RModel.Gender));
                    var protectedEmail = _protector.Protect(System.Web.HttpUtility.HtmlEncode(EmailAddress));
                    var protectedPhoneNo = _protector.Protect(System.Web.HttpUtility.HtmlEncode(RModel.PhoneNumber));
                    var protectedAddress = _protector.Protect(System.Web.HttpUtility.HtmlEncode(RModel.DeliveryAddress));
                    var protectedAboutMe = _protector.Protect(System.Web.HttpUtility.HtmlEncode(RModel.AboutMe));



                    //pass data to database
                    var user = new AuthUser
                    {
                        UserName = EmailAddress,
                        Photo = ImageURL,
                        FullName = RModel.FullName,
                        Gender = RModel.Gender,
                        Email = EmailAddress,
                        PhoneNumber = "+65 " + RModel.PhoneNumber,
                        DeliveryAddress = RModel.DeliveryAddress,
                        CreditCardNumber = protectedCreditCardNumber,
                        AboutMe = RModel.AboutMe
                    };

                    //check duplicate email
                    var enteredEmail = await userManager.FindByEmailAsync(EmailAddress);

                    if (enteredEmail != null)
                    {
                        ModelState.AddModelError(EmailAddress, "This email has already been registered");
                        return Page();
                    }

                    var result = await userManager.CreateAsync(user, Password);
                    if (result.Succeeded)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        if(user.Email == "ZHIYI@gmail.com")
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        return RedirectToPage("Login");
                    }

                    // show error message in page
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, e.Description);
                    }

                }
            }
            return Page();
        }



    }
}
