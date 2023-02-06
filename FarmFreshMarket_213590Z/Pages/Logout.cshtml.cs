using FarmFreshMarket_213590Z.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmFreshMarket_213590Z.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly IHttpContextAccessor _contxt;

        public LogoutModel(IHttpContextAccessor httpContextAccessor, SignInManager<AuthUser> signInManager)
        {
            _signInManager = signInManager;
            _contxt = httpContextAccessor;
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                _signInManager.SignOutAsync();
                _contxt.HttpContext.Session.Clear();
            }
            return RedirectToPage("Index");
        }
        //clear session
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            _signInManager.SignOutAsync();
            _contxt.HttpContext.Session.Clear();
            return RedirectToPage("Login");
        }
        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("Index");
        }

    }
}
