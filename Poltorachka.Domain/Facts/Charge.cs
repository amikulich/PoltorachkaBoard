using System;

namespace Poltorachka.Domain.Facts
{
    public class Charge : FactBase, IFact
    {
        internal Charge()
        {
        }

        public Charge(int appId, int winnerId, int loserId, int creatorId, byte score, string description, Func<int, byte> userRemainingBalanceFunc)
            : this()
        {
            Assert.That(appId > 0, "App id must be provided");
            Assert.That(winnerId > 0, "Winner id must be provided");
            Assert.That(loserId > 0, "Loser id must be provided");
            Assert.That(creatorId > 0, "Creator id must be provided");
            Assert.That(score > 0, "Score should be a postive number");

            Type = FactType.Charge;

            AppId = appId;
            WinnerId = winnerId;
            LoserId = loserId;
            CreatorId = creatorId;
            Score = score;
            Description = description;
            Date = DateTime.UtcNow;

            if (LoserId == CreatorId)
            {
                ApproverId = CreatorId;
                Status = FactStatus.Approved;
            }
            else
            {
                Status = FactStatus.Pending;
            }
        }

        public void Decline(int witnessId)
        {
            Assert.That(witnessId != LoserId, "Loser cannot decline the fact");
            Assert.That(Status == FactStatus.Pending, "A fact should be in the Pending status");

            Status = FactStatus.Canceled;
            ApproverId = witnessId;
        }

        public void Approve(int witnessId)
        {
            Assert.That(witnessId != WinnerId, "A winner cannot approve a fact");
            Assert.That(witnessId != CreatorId, "A creator cannot approve a fact");
            Assert.That(Status == FactStatus.Pending, "A fact should be in the Pending status");

            Status = FactStatus.Approved;
            ApproverId = witnessId;
        }
    }
}
