using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System;
using System.ComponentModel.DataAnnotations;

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
            Message = "Make a Reservation";
        }

        [BindProperty]
        [Required]
        public int PartySize { get; set; }

        [BindProperty]
        public DateTime Time { get; set; }

        public string Message { get; private set; }

        public void OnGet()
        {
            _logger.LogInformation("GET request received for reservation page at {Time}", DateTime.UtcNow);
            Message = "Make a Reservation";
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("POST request received for reservation submission at {Time}", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid reservation data received at {Time}. ModelState Errors: {Errors}",
                    DateTime.UtcNow, ModelState);
                return Page();
            }

            try
            {
                var reservation = new Reservation
                {
                    PartySize = PartySize,
                    UserID = 1,
                    TableID = 1,
                    Time = Time,
                    Status = "Pending",
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                _logger.LogInformation("Reservation successfully submitted for PartySize: {PartySize}, Time: {Time}",
                    PartySize, Time);

                Message = "Reservation successfully submitted!";
                return RedirectToPage("/Reservation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the reservation at {Time}", DateTime.UtcNow);
                Message = "An error occurred. Please try again.";
                return Page();
            }
        }
    }
}
