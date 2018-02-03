using System;

namespace Poltorachka.Domain.Events
{
    public class FactRegisteredEvent : DomainEvent
    {
        public string WinnerName { get; }
        public string LoserName { get; }
        public string AuditorName { get; }
        public DateTime Date { get; }

        public FactRegisteredEvent(string winnerName,
            string loserName,
            string auditorName,
            DateTime date)
        {
            WinnerName = winnerName;
            LoserName = loserName;
            AuditorName = auditorName;
            Date = date;
        }
    }
}
