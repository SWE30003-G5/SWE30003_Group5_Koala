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

        //The following properties are for test purpose with cookie, can delete later if not needed
        public int userID { get; set; }
        public string userEmail { get; set; }
        public string userName { get; set; }
        public string userPhoneNumber { get; set; }
        public string userRole { get; set; }
        public string userPassword { get; set; }

        public void OnGet()
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
                    userCookieClient = userList.First();

                    //Step 4: Assign fields from individual objects in list to variables in code, like .Role to know if the role is "Customer", "Staff", "Admin"
                    // Assign values from the first user in the list
                    userID = userCookieClient.ID;
                    userEmail = userCookieClient.Email;
                    userName = userCookieClient.Name;
                    userRole = userCookieClient.Role;
                    userPassword = userCookieClient.Password;
                }
                else
                {
                    userID = 0;
                    userEmail = "Blank";
                    userName = "Blank";
                    userRole = "Blank";
                    userPassword = "Blank";
                }
            }
            else
            {
                userID = 0;
                userEmail = "Blank";
                userName = "Blank";
                userRole = "Blank";
                userPassword = "Blank";
            }
        }

        public IActionResult OnPostLogout()
        {
            Response.Cookies.Delete("userCookie"); // Deletes the userCookie
            return RedirectToPage("/Index"); // Redirects to Login page to show changes in the navigation bar
        }
    }
}
