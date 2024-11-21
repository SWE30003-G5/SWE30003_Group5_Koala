using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.OrdersManager
{
    public class EditModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(KoalaDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "In Process", Value = "In Process" },
            new SelectListItem { Text = "Completed", Value = "Completed" },
            new SelectListItem { Text = "Canceled", Value = "Canceled" }
        };
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  await _context.Orders.FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }
            Order = order;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning("ModelState Error: {ErrorMessage}", error.ErrorMessage);
                }
                return Page();
            }

            _logger.LogInformation("Editing an order (ID = {ID})...", Order.ID);

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Edited the order (ID = {ID})", Order.ID);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogInformation("An exception occurring while the order (ID = {ID}) is being edited", Order.ID);
                if (!OrderExists(Order.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
