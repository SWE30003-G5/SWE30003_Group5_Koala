using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.OrdersManager
{
    public class DetailsModel : PageModel
    {
        private readonly SWE30003_Group5_Koala.Data.KoalaDbContext _context;

        public DetailsModel(SWE30003_Group5_Koala.Data.KoalaDbContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;
        public IList<OrderItem> OrderItems { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            OrderItems = await _context.OrderItems
                .Include(oi => oi.MenuItem)
                .Where(oi => oi.OrderID == id)
                .ToListAsync();
            return Page();
        }
    }
}
