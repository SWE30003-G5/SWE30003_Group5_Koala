using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.UsersManager
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
        public User User { get; set; } = default!;
        public List<SelectListItem> RoleOption { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Admin", Value = "Admin" },
            new SelectListItem { Text = "Customer", Value = "Customer" },
            new SelectListItem { Text = "Staff", Value = "Staff" }
        };
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =  await _context.Users.FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
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

            _logger.LogInformation($"Attempting to update User with ID {User.ID} and Role {User.Role}");

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"User with ID {User.ID} successfully updated.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.ID))
                {
                    _logger.LogWarning($"User with ID {User.ID} does not exist.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Unexpected concurrency exception while updating the User.");
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
