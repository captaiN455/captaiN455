using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FarmFreshMarket_213590Z.Pages
{
    [Authorize(Roles ="Admin")]
    public class TestLogModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
