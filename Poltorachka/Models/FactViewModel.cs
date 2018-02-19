using System;

namespace Poltorachka.Models
{
    public class FactViewModel
    {
        public int FactId { get; set; }

        public string WinnerName { get; set; }

        public string LoserName { get; set; }

        public string CreatorName { get; set; }

        public string ApproverName { get; set; }

        public byte Score { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public FactStatusViewModel Status { get; set; }
    }

    public enum FactStatusViewModel
    {
        Pending = 1,

        Approved = 2,

        Expired = 3,

        Canceled = 4,
    }
}
