using System;

namespace Poltorachka.Web.Models
{
    public class FactEditViewModel
    {
        public int FactId { get; set; }

        public int WinnerId { get; set; }

        public string WinnerName { get; set; }

        public int LoserId { get; set; }

        public string LoserName { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

        public int? WitnessId { get; set; }

        public string WitnessName { get; set; }

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
