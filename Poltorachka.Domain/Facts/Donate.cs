using System;

namespace Poltorachka.Domain.Facts
{
    public class Donate : FactBase, IFact
    {
        internal Donate()
        {
        }

        public Donate(int appId, 
            int winnerId, 
            int grantorId, 
            byte score, 
            string description, 
            Func<int, byte> userRemainingBalanceFunc)
        {
            Assert.NotNull(userRemainingBalanceFunc, nameof(userRemainingBalanceFunc));

            Assert.That(appId > 0, "App id must be provided");
            Assert.That(winnerId > 0, "Winner id must be provided");
            Assert.That(grantorId > 0, "Loser id must be provided");
            Assert.That(score > 0, "Score should be a postive number");

            Type = FactType.Donate;

            AppId = appId;
            WinnerId = winnerId;
            LoserId = grantorId;
            CreatorId = grantorId;
            Score = score;
            Description = description;
            Date = DateTime.UtcNow;
            Status = FactStatus.Pending;

            Assert.That(userRemainingBalanceFunc(grantorId) >= score, "Month limit exceeded for this user");
        }

        public void Approve(int witnessId)
        {
            Assert.That(witnessId != WinnerId, "A winner cannot approve a fact");
            Assert.That(witnessId != LoserId, "A creator cannot approve a fact");
            Assert.That(Status == FactStatus.Pending, "A fact should be in the Pending status");

            Status = FactStatus.Approved;
            ApproverId = witnessId;
        }

        public void Decline(int witnessId)
        {
            Assert.That(witnessId != LoserId, "Loser cannot decline the fact");
            Assert.That(Status == FactStatus.Pending, "A fact should be in the Pending status");

            Status = FactStatus.Canceled;
            ApproverId = witnessId;
        }
    }
}
