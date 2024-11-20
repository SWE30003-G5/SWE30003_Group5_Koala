using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System.Text.Json;

namespace SWE30003_Group5_Koala.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(KoalaDbContext context, ILogger<RegisterModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public User User { get; set; } = new();

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnPost()
        {
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return Page(); // Stay on the page and show validation errors
            }

            // Check if passwords match
            if (User.Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page(); // Stay on the page and show the error
            }

            // Check if the email already exists in the database
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == User.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("User.Email", "This email is already registered.");
                return Page(); // Stay on the page and show the error
            }


            _context.Users.Add(User);
            _context.SaveChanges();

            return RedirectToPage("/Login"); // Redirect to login page after successful registration
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