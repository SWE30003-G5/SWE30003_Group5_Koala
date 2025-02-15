using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages
{
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