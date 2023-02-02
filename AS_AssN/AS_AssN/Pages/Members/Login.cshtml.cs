using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AS_AssN.Models;


namespace AS_AssN.Pages.Members
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<AppUser> signInManager;
        public LoginModel(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.UserName, LModel.Password,
                LModel.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    /*                    var userId = signInManager.UserManager.Users.FirstOrDefault()?.Id; 
                    */
                    return RedirectToPage("Profile");
                }

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
            }
            return Page();
        }

        }
}