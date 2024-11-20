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

        public IActionResult OnPost()
        {
            //Console.WriteLine(userEmail);
            //Console.WriteLine(userPassword); 

            var returnedUser = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).ToList();

            if (returnedUser.Any())
            {
                var cookieOptions = new CookieOptions
                {
                    Secure = true, // Ensure secure transmission in production
                    HttpOnly = false, // Allow JavaScript access to the cookie if needed
                    SameSite = SameSiteMode.Lax, // Mitigate CSRF attacks
                    Path = "/", // Ensure cookie is accessible across the site
                    Expires = DateTime.Now.AddDays(30) // Expiration period
                };

                var userConvertJson = JsonSerializer.Serialize(returnedUser);

                // Add the cookie to the response cookie collection
                Response.Cookies.Append("userCookie", userConvertJson, cookieOptions);
                return RedirectToPage("/Index");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid login credentials. Please try again.";
                return Page();
            }
        }

        //This is to stop user from login or register when have cookie
        public IActionResult OnGet()
        {
            Models.User userCookieClient = new();
            var cookieJson = Request.Cookies["userCookie"]; //Step 1: get the json file from cookie

            if (cookieJson != null)
            {
                // Deserialize the JSON array into a list of users. Because we use ToList in Login.cshtml.cs
                //Step 2: Deserialize the json of cookie into list
                var userList = JsonSerializer.Deserialize<List<Models.User>>(cookieJson);

                //Step 3: Check if list has any cookies(objects) in them then user First() to get the first object in the list
                // Safely check if the list contains at least one user
                if (userList != null && userList.Any())
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }
    }
}