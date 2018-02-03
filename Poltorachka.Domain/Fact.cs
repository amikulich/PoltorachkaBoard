using System;
using Poltorachka.Domain.Events;

namespace Poltorachka.Domain
{
    public class Fact : DomainEntity
    {
        protected Fact()
        {
        }

        public Fact(string winnerName, 
            string loserName, 
            string auditorName, 
            byte score)
            : this()
        {
            WinnerName = winnerName;
            LoserName = loserName;
            AuditorName = auditorName;
            Score = score;
            Date = DateTime.UtcNow;

            if (LoserName == AuditorName)
            {
                Status = FactStatus.Approved;
            }
            else
            {
                Status = FactStatus.Registered;
                Events.Add(new FactRegisteredEvent(winnerName, loserName, auditorName, Date));
            }
        }

        public string WinnerName { get; }

        public string LoserName { get; }

        public string AuditorName { get; }

        public byte Score { get; }

        public FactStatus Status { get; }

        public DateTime Date { get; set; }
    }
}
