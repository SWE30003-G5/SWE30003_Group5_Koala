using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SWE30003_Group5_Koala.Pages
{
    public class UnauthorizedAccessModel : PageModel
    {
        public void OnGet()
        {
            Response.StatusCode = 403;
        }
    }
}
