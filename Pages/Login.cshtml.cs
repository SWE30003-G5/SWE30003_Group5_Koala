using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SWE30003_Group5_Koala.Pages
{
    public class LoginModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string userEmail { get; set; } //for the input email in the login page
        [BindProperty]
        public string userPassword { get; set; } //Same as userEmail

        public LoginModel(KoalaDbContext context, ILogger<LoginModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnPost()
        {
            //Console.WriteLine(userEmail);
            //Console.WriteLine(userPassword); 

            var returnedUser = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).ToList();

            if (returnedUser.Any())
            {
                var cookieOptions = new CookieOptions
                {
                    // Set the secure flag, which Chrome's changes will require for SameSite none.
                    // Note this will also require you to be running on HTTPS
                    Secure = true,

                    // Set the cookie to HTTP only which is good practice unless you really do need
                    // to access it client side in scripts.
                    HttpOnly = true,

                    // Add the SameSite attribute, this will emit the attribute with a value of none.
                    // To not emit the attribute at all set the SameSite property to SameSiteMode.Unspecified.
                    SameSite = SameSiteMode.Lax,

                    //Change to set when cookie expires
                    Expires = DateTime.Now.AddDays(30),
                };

                var userConvertJson = JsonSerializer.Serialize(returnedUser);

                // Add the cookie to the response cookie collection
                Response.Cookies.Append("userCookie", userConvertJson, cookieOptions);
            }
            else
            {
                ViewData["ErrorMessage"] = "Check your information again";
            }
        }
    }
}