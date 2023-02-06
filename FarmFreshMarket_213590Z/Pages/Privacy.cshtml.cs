using FarmFreshMarket_213590Z.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Lib;

namespace FarmFreshMarket_213590Z.Pages
{
    
    public class PrivacyModel : PageModel
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IHttpContextAccessor _http;
        public AuthUser retrieveuser { get; set; } = new();
        public string username { get; set; }


        public PrivacyModel(IHttpContextAccessor http, UserManager<AuthUser> userManager)
        {
            _userManager = userManager;
            _http = http;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            //retrieve the Session Data
            username = _http.HttpContext?.Session.GetString(SessionVariable.UserName);
            if (username == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}