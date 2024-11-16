using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages
{
    public class LoginModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<MenuModel> _logger;
        public LoginModel(KoalaDbContext context, ILogger<MenuModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
