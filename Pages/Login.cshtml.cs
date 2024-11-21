using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWE30003_Group5_Koala.Data;
using System.Text.Json;

namespace SWE30003_Group5_Koala.Pages
{
    public class LoginModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string userEmail { get; set; }
        [BindProperty]
        public string userPassword { get; set; }

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
            var cookieJson = Request.Cookies["userCookie"];

            if (cookieJson != null)
            {
                var userList = JsonSerializer.Deserialize<List<Models.User>>(cookieJson);

                if (userList != null && userList.Any())
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }
    }
}