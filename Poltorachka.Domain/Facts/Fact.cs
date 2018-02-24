using System;
using System.Runtime.CompilerServices;
using Poltorachka.Domain.Facts;
using Poltorachka.Domain.Primitives;

[assembly:InternalsVisibleTo("Poltorachka.DataAccess")]

namespace Poltorachka.Domain
{
    public class Fact : DomainEntity
    {
        internal Fact()
        {
        }

        public Fact(int appId, int winnerId, int loserId, int creatorId, byte score, string description, Func<int, byte> userRemainingBalanceFunc)
            : this()
        {
            Assert.NotNull(userRemainingBalanceFunc, nameof(userRemainingBalanceFunc));

            Assert.That(appId > 0, "App id must be provided");
            Assert.That(winnerId > 0, "Winner id must be provided");
            Assert.That(loserId > 0, "Loser id must be provided");
            Assert.That(creatorId > 0, "Creator id must be provided");

            Assert.That(userRemainingBalanceFunc(loserId) >= score, "Month limit exceeded for this user");

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

        public int AppId { get; internal set; }

        public int FactId { get; internal set; }

        public int WinnerId { get; internal set; }

        public int LoserId { get; internal set; }

        public int CreatorId { get; internal set; }

        public int? ApproverId { get; internal set; }

        public byte Score { get; internal set; }

        public string Description { get; internal set; }

        public FactStatus Status { get; internal set; }

        public DateTime Date { get; internal set; }

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
