using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SWE30003_Group5_Koala.Pages
{
    // The following is just a template to test how Razor Pages work, feel free to delete/modify it.
    public class MenuModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<MenuModel> _logger;
        public MenuModel(KoalaDbContext context, ILogger<MenuModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IList<MenuItem> MenuItems { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.MenuItems == null)
            {
                _logger.LogError("The Menu Items context is null.");
                return;
            }
            else
            {
                MenuItems = await _context.MenuItems.ToListAsync();
            }
            if (MenuItems.Count == 0)
            {
                _logger.LogInformation("No menu items found.");
            }
        }
    }
}