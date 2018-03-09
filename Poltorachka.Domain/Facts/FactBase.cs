using System;
using System.Runtime.CompilerServices;
using Poltorachka.Domain.Primitives;

[assembly: InternalsVisibleTo("Poltorachka.DataAccess")]

namespace Poltorachka.Domain.Facts
{
    public abstract class FactBase : DomainEntity
    {
        internal int AppId { get; set; }

        internal int FactId { get; set; }

        internal int WinnerId { get; set; }

        internal int LoserId { get; set; }

        internal int CreatorId { get; set; }

        internal int? ApproverId { get; set; }

        internal byte Score { get; set; }

        internal string Description { get; set; }

        internal FactStatus Status { get; set; }

        internal FactType Type { get; set; }

        internal DateTime Date { get; set; }

        public abstract void Approve(int witnessId);

        public abstract void Decline(int witnessId);
    }
}
