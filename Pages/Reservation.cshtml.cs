using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWE30003_Group5_Koala.Pages
{
    public class ReservationModel : PageModel
    {
        // The following is just a template to show how Razor Pages works, feel free to delete/modify it.
        public string Message { get; private set; }

        public void OnGet()
        {
            Message = "Hello Reservation from OnGet!";
        }
        public async Task<IActionResult> OnPostAsync() { return Page(); }
    }
}
