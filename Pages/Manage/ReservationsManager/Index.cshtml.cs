using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.ReservationsManager
{
    public class IndexModel : PageModel
    {
        private readonly KoalaDbContext _context;

        public IndexModel(KoalaDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.Table)
                .Include(r => r.User).ToListAsync();
        }
    }
}
