using System;
using Poltorachka.Domain.Events;

namespace Poltorachka.Domain
{
    public class Fact : DomainEntity
    {
        public Fact()
        {
        }

        public Fact(string winnerName, 
            string loserName, 
            string creatorName, 
            byte score)
            : this()
        {
            WinnerName = winnerName;
            LoserName = loserName;
            CreatorName = creatorName;
            Score = score;
            Date = DateTime.UtcNow;

            if (LoserName == CreatorName)
            {
                ApproverName = CreatorName;
                Status = FactStatus.Approved;
            }
            else
            {
                Status = FactStatus.Registered;
                Events.Add(new FactRegisteredEvent(winnerName, loserName, creatorName, Date));
            }
        }

        public void Approve(string approverName)
        {
            if (approverName != WinnerName)
            {
                Status = FactStatus.Approved;
                return;
            }
            
            throw new InvalidOperationException("Winner cannot approve the fact");
        }


        public int FactId { get; set; }

        public string WinnerName { get; set; }

        public string LoserName { get; set; }

        public string CreatorName { get; set; }

        public string ApproverName { get; set; }

        public byte Score { get; set; }

        public FactStatus Status { get; set; }

        public DateTime Date { get; set; }
    }
}
