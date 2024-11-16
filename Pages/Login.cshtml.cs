using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System.Diagnostics;

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

            var sth = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword).ToList();
        }
    }
}