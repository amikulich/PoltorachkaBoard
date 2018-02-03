using System;

namespace Poltorachka.Domain
{
    public class FactReadModel
    {
        public string WinnerName { get; set; }

        public string LoserName { get; set; }

        public string AuditorName { get; set; }

        public byte Score { get; set; }

        public FactStatus Status { get; set; }

        public DateTime Date { get; set;}
    }
}
