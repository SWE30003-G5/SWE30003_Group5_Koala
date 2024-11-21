using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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

        [BindProperty]
        public int PartySize { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [BindProperty]
        [DataType(DataType.Time)]
        public TimeSpan ReservationTime { get; set; }

        public List<Reservation> ReservationHistory { get; set; }
        public bool IsLoggedIn { get; set; }

        public void OnGet()
        {
            var userCookie = Request.Cookies["userCookie"];
            int? userID = null;

            if (!string.IsNullOrEmpty(userCookie))
            {
                try
                {
                    var users = JsonSerializer.Deserialize<List<User>>(userCookie);

                    if (users != null && users.Count > 0)
                    {
                        userID = users[0].ID;
                        IsLoggedIn = true;
                    }
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error parsing userCookie.");
                }
            }

            if (userID.HasValue)
            {
                // Get the reservation history for the user from the database
                ReservationHistory = _context.Reservations
                    .Where(r => r.UserID == userID.Value)
                    .OrderByDescending(r => r.Time)  // Show most recent first
                    .ToList();
            }
            else
            {
                _logger.LogWarning("UserID is missing from cookie.");
                IsLoggedIn = false;
            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid reservation data received. ModelState Errors: {Errors}", ModelState);
                return Page();
            }

            var combinedDateTime = ReservationDate.Add(ReservationTime);

            if (combinedDateTime < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "The reservation date and time cannot be in the past.");
                return Page();
            }


            var userCookie = Request.Cookies["userCookie"];
            int? userID = null;

            if (!string.IsNullOrEmpty(userCookie))
            {
                try
                {
                    var users = JsonSerializer.Deserialize<List<User>>(userCookie);

                    if (users != null && users.Count > 0)
                    {
                        userID = users[0].ID;
                    }
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, "Error parsing userCookie.");
                    return BadRequest("Invalid user data.");
                }
            }

            if (!userID.HasValue)
            {
                _logger.LogWarning("UserID is missing from cookie.");
                return Unauthorized();
            }

            try
            {
                var reservation = new Reservation
                {
                    PartySize = PartySize,
                    UserID = userID.Value,
                    TableID = 1,
                    Time = combinedDateTime,
                    Status = "Pending",
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                _logger.LogInformation("Reservation successfully submitted: PartySize={PartySize}, Time={Time}, UserID={UserID}",
                    PartySize, combinedDateTime, userID.Value);

                return RedirectToPage("/Reservation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the reservation.");
                return Page();
            }
        }
    }
}
