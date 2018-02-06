using System;

namespace Poltorachka.Domain
{
    public class FactReadModel
    {
        public int FactId { get; set; }

        public string WinnerName { get; set; }

        public string LoserName { get; set; }

        public string ApproverName { get; set; }

        public string CreatorName { get; set; }

        public byte Score { get; set; }

        public FactStatus Status { get; set; }

        public DateTime Date { get; set;}
    }
}
