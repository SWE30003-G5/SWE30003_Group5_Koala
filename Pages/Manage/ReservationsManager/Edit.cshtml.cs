using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

namespace SWE30003_Group5_Koala.Pages.Manage.ReservationsManager
{
    public class EditModel : PageModel
    {
        private readonly SWE30003_Group5_Koala.Data.KoalaDbContext _context;
        private readonly ILogger<EditModel> _logger;
        public EditModel(SWE30003_Group5_Koala.Data.KoalaDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public List<SelectListItem> StatusOption { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Pending", Value = "Pending" },
            new SelectListItem { Text = "Confirmed", Value = "Confirmed" },
            new SelectListItem { Text = "Cancelled", Value = "Cancelled" }
        };
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation =  await _context.Reservations.FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            Reservation = reservation;
            if (Reservation.TableID != null)
            {
                ViewData["TableID"] = new SelectList(
                    await _context.Tables.Where(t => t.IsAvailable || t.Id == Reservation.TableID).ToListAsync(),
                    "Id",
                    "Id",
                    Reservation.TableID
                );
            }
            else
            {
                ViewData["TableID"] = new SelectList(
                    await _context.Tables.Where(t => t.IsAvailable).ToListAsync(),
                    "Id",
                    "Id"
                );
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reservation).State = EntityState.Modified;

            try
            {
                if (Reservation.TableID != null)
                {
                    var currentTable = await _context.Tables.FirstOrDefaultAsync(t => t.Id == Reservation.TableID);
                    if (currentTable != null)
                    {
                        var originalAvailability = currentTable.IsAvailable;

                        if (Reservation.Status == "Confirmed" || Reservation.Status == "Cancelled")
                        {
                            _logger.LogInformation("Reservation status is {Status}. Setting table availability to true.", Reservation.Status);
                            currentTable.IsAvailable = true;
                        }
                        else if (!originalAvailability)
                        {
                            _logger.LogInformation("Table was not originally available. Setting table availability to true.");
                            currentTable.IsAvailable = true;
                            if (!currentTable.IsAvailable)
                            {
                                ModelState.AddModelError("Reservation.TableID", "The selected table is no longer available.");
                                currentTable.IsAvailable = originalAvailability;
                                ViewData["TableID"] = new SelectList(
                                    await _context.Tables.Where(t => t.IsAvailable).ToListAsync(),
                                    "Id",
                                    "Id"
                                );
                                ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
                                _logger.LogWarning("Table is no longer available. Reverting availability and returning page.");
                                return Page();
                            }
                        }
                        else
                        {
                            currentTable.IsAvailable = false;
                        }
                        _context.Attach(currentTable).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(Reservation.Id))
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

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
