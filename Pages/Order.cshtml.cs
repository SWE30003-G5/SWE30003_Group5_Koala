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
        public IList<MenuItem> MenuItems { get; set; } = default!;
        [BindProperty]
        public string OrderType { get; set; }
        [BindProperty]
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        [BindProperty]
        public decimal TotalAmount { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.MenuItems == null)
            {
                _logger.LogError("The Menu Items context is null.");
                return;
            }
            else {

                MenuItems = await _context.MenuItems.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var order = new Order
            {
                Date = DateTime.Now,
                Type = OrderType,
                TotalAmount = TotalAmount,
                UserID = 1, // Dummy UserID
                Status = "In Process"
            };
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order stored successfully: ID={ID}, Date={Date}, Type={Type}, TotalAmount={TotalAmount}, UserID={UserID}, Status={Status}",
                    order.ID, order.Date, order.Type, order.TotalAmount, order.UserID, order.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while storing the order.");
                return Page();
            }

            return RedirectToPage("/Order", new { id = order.ID });
        }
    }
}