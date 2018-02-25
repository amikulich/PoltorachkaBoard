using Microsoft.AspNetCore.Mvc;

namespace Poltorachka.Web.Pages.Profile
{
    public class ProfileEditModel : PageViewModelBase
    {
        public IActionResult OnGet(string userName)
        {
            if (!string.Equals(UserName, userName))
            {
                return RedirectToPage("/Profile/ProfileEdit", new {userName = UserName});
            }

            return Page();
        }
    }
}