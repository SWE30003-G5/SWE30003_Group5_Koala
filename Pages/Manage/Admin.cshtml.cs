using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage
{
    public class AdminModel : PageModel
    {
        public string UserRole { get; set; }

        public void OnGet()
        {
            var userCookie = Request.Cookies["userCookie"];
            if (!string.IsNullOrEmpty(userCookie))
            {
                var users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userCookie);
                UserRole = users?.FirstOrDefault()?.Role;
            }
        }
    }
}
