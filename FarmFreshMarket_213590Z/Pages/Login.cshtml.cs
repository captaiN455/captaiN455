using FarmFreshMarket_213590Z.Models;
using FarmFreshMarket_213590Z.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Lib;

namespace FarmFreshMarket_213590Z.Pages
{

    public class LoginModel : PageModel
    {


        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        //session
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly GoogleCaptchaService _captchaservice;
        public LoginModel(SignInManager<AuthUser> signInManager, IHttpContextAccessor httpContextAccessor, GoogleCaptchaService captchaservice, UserManager<AuthUser> userManager)
        {
            _signInManager = signInManager;
            //session
            _httpContextAccessor = httpContextAccessor;
            _captchaservice = captchaservice;
            _userManager = userManager;
        }

        [BindProperty]
        public Login LModel { get; set; }


        [AllowAnonymous]
        public void OnGet()
        {

        }

        /*public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("OnPostGoogle")

            });
            Task Challenge(_signInManager.ConfigureExternalAuthenticationProperties("Google", "/GoogleLogin"), "Google");
        }*/

        public async Task<IActionResult> OnPostGoogle()
        {
            return Challenge(_signInManager.ConfigureExternalAuthenticationProperties("Google", "/GoogleLogin"), "Google");
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            return (IActionResult)claims;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostInHouse()
        {


            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is INVALID");
                return Page();
            }

            Console.WriteLine("Signing in");
            var identityResult = await _signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
            if (identityResult.Succeeded)
            {
                Console.WriteLine("Success");


                _httpContextAccessor.HttpContext.Session.SetString(SessionVariable.UserName, LModel.Email);

                return RedirectToPage("Index");
            }
            // Account lockout
            else if (identityResult.IsLockedOut)
            {
                //var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);
                //var message = new Message(new string[] { userModel.Email }, "Locked out account information", content, null);
                //await _emailSender.SendEmailAsync(message);
                ModelState.AddModelError("", "The account is locked out");
                return Page();
            }
            Console.WriteLine("Failed");
            ModelState.AddModelError(nameof(LModel.Email), "Username or Password is incorrect");
            return Page();
        }





    }
}
