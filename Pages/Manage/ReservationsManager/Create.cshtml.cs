using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.ReservationsManager
{
    public class CreateModel : PageModel
    {
        private readonly KoalaDbContext _context;

        public CreateModel(KoalaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["TableID"] = new SelectList(_context.Tables, "Id", "Id");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reservation.TableID = 1;

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
