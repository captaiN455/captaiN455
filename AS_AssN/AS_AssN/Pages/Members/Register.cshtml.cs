using AS_AssN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace AS_AssN.Pages.Members
{
    public class RegisterModel : PageModel
    {


        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }
        [BindProperty]
        public Registeration RModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Gender { get; set; }
        public string[] Genders = new[] { "Male", "Female"};



    public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }


        public void OnGet()
        {
        }
    }
}
