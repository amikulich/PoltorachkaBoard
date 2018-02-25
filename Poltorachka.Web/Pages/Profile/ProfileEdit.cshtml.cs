using Microsoft.AspNetCore.Mvc;
using Poltorachka.Web.Services;

namespace Poltorachka.Web.Pages.Profile
{
    public class ProfileEditModel : PageViewModelBase
    {
        private readonly IProfileStatsAppService _profileStatsAppService;

        public ProfileEditModel(IProfileStatsAppService profileStatsAppService)
        {
            _profileStatsAppService = profileStatsAppService;
        }

        public IActionResult OnGet(string userName)
        {
            ViewData["Title"] = @"Профиль";

            if (!string.Equals(UserName, userName))
            {
                return RedirectToPage("/Profile/ProfileEdit", new {userName = UserName});
            }

            var stats = _profileStatsAppService.Get(UserId);

            MonthlyScore = stats.MonthlyScore;
            MonthlyDonatesLeft = stats.MonthlyDonatesLeft;
            MonthlyPeopleReached = stats.MonthlyPeopleReached;

            OverallScore = stats.OverallScore;
            OverallPlace = stats.OverallPosition;

            return Page();
        }

        public int MonthlyScore { get; private set; }

        public int MonthlyDonatesLeft { get; private set; }

        public int MonthlyPeopleReached { get; private set; }

        public int OverallScore { get; private set; }

        public int OverallPlace { get; private set; }
    }
}