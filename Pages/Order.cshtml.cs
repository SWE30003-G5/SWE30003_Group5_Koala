using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages
{
    public class OrderModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<OrderModel> _logger;
        public OrderModel(KoalaDbContext context, ILogger<OrderModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IList<Order> Orders { get; set; } = default!;
        public IList<MenuItem> MenuItems { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Orders == null)
            {
                _logger.LogError("The Orders context is null.");
                return;
            }
            else {
                Orders = await _context.Orders.ToListAsync();
                MenuItems = await _context.MenuItems.ToListAsync();
            }
            if (Orders.Count == 0)
            {
                _logger.LogInformation("No orders found.");
            }
        }
    }
}