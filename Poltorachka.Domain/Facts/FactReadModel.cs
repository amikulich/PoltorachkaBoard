using System;

namespace Poltorachka.Domain.Facts
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
            FactType type,
            string description, 
            DateTime date, 
            FactStatus status)
        {
            Type = type;
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

        public FactType Type { get; internal set; }

        public int FactId { get; internal set; }

        public int WinnerId { get; internal set; }

        public string WinnerName { get; internal set; }

        public int LoserId { get; internal set; }

        public string LoserName { get; internal set; }

        public int CreatorId { get; internal set; }

        public string CreatorName { get; internal set; }

        public int? WitnessId { get; internal set; }

        public string WitnessName { get; internal set; }

        public byte Score { get; internal set; }

        public string Description { get; internal set; }

        public DateTime Date { get; internal set; }

        public FactStatus Status { get; internal set; }
    }
}
