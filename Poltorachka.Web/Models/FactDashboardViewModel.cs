using System;

using Poltorachka.Web.Pages.Facts;

namespace Poltorachka.Web.Models
{
    public class FactDashboardViewModel
    {
        public int FactId { get; set; }

        public string WinnerName { get; set; }

        public string LoserName { get; set; }

        public string CreatorName { get; set; }

        public string WitnessName { get; set; }

        public byte Score { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public FactStatusViewModel Status { get; set; }

        public FactTypeModelEnum Type { get; set; }
    }
}
