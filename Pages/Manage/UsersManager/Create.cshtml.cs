using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.UsersManager
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
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        public List<SelectListItem> RoleOption { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "--Select Role--", Value = "", Disabled = true, Selected = true },
            new SelectListItem { Text = "Admin", Value = "Admin" },
            new SelectListItem { Text = "Customer", Value = "Customer" },
            new SelectListItem { Text = "Staff", Value = "Staff" }
        };

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
