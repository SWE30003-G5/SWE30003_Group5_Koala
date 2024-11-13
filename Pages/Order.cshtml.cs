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
        public decimal TotalAmount { get; set; }
        [BindProperty]
        public string OrderItemsJson { get; set; }
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
                _logger.LogWarning("Invalid order data.");
                return Page();
            }
            if (string.IsNullOrEmpty(OrderItemsJson))
            {
                _logger.LogWarning("Order items data is empty.");
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
                return StatusCode(500, "An error occurred while processing your order.");
            }
            try
            {
                var orderItems = System.Text.Json.JsonSerializer.Deserialize<List<OrderItem>>(OrderItemsJson);

                if (orderItems == null)
                {
                    _logger.LogError("Deserialized orderItems is null.");
                    return BadRequest("Invalid order items data.");
                }

                foreach (var item in orderItems)
                {
                    var menuItem = await _context.MenuItems.FindAsync(item.MenuItemID);

                    if (menuItem != null)
                    {
                        item.OrderID = order.ID;
                        item.SubTotal = menuItem.Price * item.Quantity;
                        _context.OrderItems.Add(item);
                    }
                    else
                    {
                        _logger.LogWarning($"MenuItem with ID {item.MenuItemID} not found.");
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing order items.");
                return StatusCode(500, "An error occurred while processing your order.");
            }
            return RedirectToPage("/Order", new { id = order.ID });
        }
    }
}