using FarmFreshMarket_213590Z.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Xamarin.Essentials;
using FarmFreshMarket_213590Z.Models;

namespace FarmFreshMarket_213590Z.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AuthUser> _signInManager;

        public HomeController(IHttpContextAccessor contextAccessor, SignInManager<AuthUser> signInManager)
        {
            _httpContextAccessor = contextAccessor;
            _signInManager = signInManager;

        }

        public IActionResult Index()
        {
            return View();
        }



       

        //public IActionResult OnSessionExpired()
        //{

        //    _signInManager.SignOutAsync();
        //    _httpContextAccessor.HttpContext.Session.Clear();

        //    return RedirectToAction("Index", "Home");
        //}


    }
}
