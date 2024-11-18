using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SWE30003_Group5_Koala.Pages
{
    public class ReservationModel : PageModel
    {
        private readonly KoalaDbContext _context;
        private readonly ILogger<ReservationModel> _logger;

        public ReservationModel(KoalaDbContext context, ILogger<ReservationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Properties bound to form fields
        [BindProperty]
        [Required, StringLength(50)]
        public string Name { get; set; }

        [BindProperty]
        [Required, Phone]
        public string Phone { get; set; }

        [BindProperty]
        [Required, EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required, Range(1, 20, ErrorMessage = "Party size must be between 1 and 20.")]
        public int PartySize { get; set; }

        [BindProperty]
        [Required]
        public string Time { get; set; } // Store as string to simplify example, convert to DateTime if needed

        [BindProperty]
        [Required]
        public DateTime Date { get; set; }

        public string Message { get; private set; }

        public void OnGet()
        {
            Message = "Make a Reservation";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid reservation data.");
                return Page();
            }

            // Check for an available table
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.IsAvailable);

            if (table == null)
            {
                _logger.LogWarning("No available table found for reservation.");
                ModelState.AddModelError(string.Empty, "No tables are currently available. Please try again later.");
                return Page();
            }

            //change into getting user from cookie
            // Create a new user for the reservation (assuming User is a separate entity)
            var user = new User
            {
                ID = 123,
                Name = Name,
                Role = "Customer",
                Email = Email,
                PhoneNumber = Phone
            };

            // Create reservation with the available table
            var reservation = new Reservation
            {
                UserID = user.ID,
                User = user,
                PartySize = PartySize,
                Status = "Pending",
                Table = table,
                Id = 123,
                Time = DateTime.Parse(Time), // Convert string to DateTime
                TableID = table.Id,

            };

            try
            {
                // Add user and reservation to the context
                _context.Users.Add(user);
                _context.Reservations.Add(reservation);

                // Update table status to "Reserved"
                table.IsAvailable = false;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Reservation stored successfully: {Name}, {Phone}, {Email}, {PartySize}, {Time}, {Date}",
                    reservation.User.Name, reservation.User.PhoneNumber, reservation.User.Email, reservation.PartySize, reservation.Time, reservation.Date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while storing the reservation.");
                return StatusCode(500, "An error occurred while processing your reservation.");
            }

            Message = "Reservation successfully submitted!";
            return RedirectToPage("/ReservationConfirmation"); // Redirect to a confirmation page if available
        }
    }
}
