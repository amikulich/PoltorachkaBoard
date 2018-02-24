using System;

namespace Poltorachka.Domain
{
    public class FactReadModel
    {
        internal FactReadModel()
        {
        }

        public FactReadModel(int factId, 
            int winnerId, 
            string winnerName, 
            int loserId, 
            string loserName, 
            int creatorId, 
            string creatorName, 
            int? witnessId, 
            string witnessName, 
            byte score, 
            string description, 
            DateTime date, 
            FactStatus status)
        {
            FactId = factId;
            WinnerId = winnerId;
            WinnerName = winnerName;
            LoserId = loserId;
            LoserName = loserName;
            CreatorId = creatorId;
            CreatorName = creatorName;
            WitnessId = witnessId;
            WitnessName = witnessName;
            Score = score;
            Description = description;
            Date = date;
            Status = status;
        }

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

        public FactStatus Status { get; set; }
    }
}
