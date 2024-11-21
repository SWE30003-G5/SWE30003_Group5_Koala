using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IList<Order> OrderHistory { get; set; } = default!;
        
        public bool IsLoggedIn { get; set; }

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

            MenuItems = await _context.MenuItems.ToListAsync();

            var userID = GetUserIDFromCookie();

            if (!userID.HasValue)
            {
                IsLoggedIn = false;
                _logger.LogWarning("UserID is missing from cookie.");
                return;
            }

            // Fetch the orders for the logged-in user
            OrderHistory = await _context.Orders
                .Where(o => o.UserID == userID.Value)
                .OrderByDescending(o => o.Date)
                .ToListAsync();
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

            var userID = GetUserIDFromCookie();

            if (!userID.HasValue)
            {
                _logger.LogWarning("UserID is missing from cookie.");
                return Unauthorized();
            }

            var order = new Order
            {
                Date = DateTime.Now,
                Type = OrderType,
                TotalAmount = TotalAmount,
                UserID = userID.Value,
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

        private int? GetUserIDFromCookie()
        {
            var userCookie = Request.Cookies["userCookie"];
            int? userID = null;

            if (!string.IsNullOrEmpty(userCookie))
            {
                try
                {
                    var users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userCookie);
                    if (users != null && users.Count > 0)
                    {
                        userID = users[0].ID;
                        IsLoggedIn = true;
                    }
                }
                catch (System.Text.Json.JsonException ex)
                {
                    _logger.LogError(ex, "Error parsing userCookie.");
                }
            }

            return userID;
        }
    }
}