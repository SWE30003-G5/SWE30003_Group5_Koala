using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.TablesManager
{
    public class CreateModel : PageModel
    {
        private readonly SWE30003_Group5_Koala.Data.KoalaDbContext _context;

        public CreateModel(SWE30003_Group5_Koala.Data.KoalaDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Table Table { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
