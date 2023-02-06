using FarmFreshMarket_213590Z.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Lib;

namespace FarmFreshMarket_213590Z.Pages
{
    
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly IDataProtector _protector;
        private readonly IHttpContextAccessor _http;

        public AuthUser retrieveuser { get; set; }
        public string username { get; set; }


        public UserProfileModel(IHttpContextAccessor httpAccessor,UserManager<AuthUser> userManager, IDataProtectionProvider DataProProvider)
        {
            _userManager = userManager;
            _protector = DataProProvider.CreateProtector("credit_card_protector");
            _http = httpAccessor;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //retrieve the Session Data
            username = _http.HttpContext?.Session.GetString(SessionVariable.UserName);
            if (username == null)
            {
                return RedirectToPage("Index");
            }
            var finduser = await _userManager.FindByEmailAsync(username);


          //decrypt the Credit Card
            var decryptedCreditCardNumber = _protector.Unprotect(finduser.CreditCardNumber);
            finduser.CreditCardNumber = decryptedCreditCardNumber;
            retrieveuser = finduser;

           
       

            return Page();
        }
       
    }
}
