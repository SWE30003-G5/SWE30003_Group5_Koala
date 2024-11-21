using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Text.Json;

namespace SWE30003_Group5_Koala.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {}

        public IActionResult OnPostLogout()
        {
            Response.Cookies.Delete("userCookie"); // Deletes the userCookie
            return RedirectToPage("/Index"); // Redirects to Login page to show changes in the navigation bar
        }
    }
}