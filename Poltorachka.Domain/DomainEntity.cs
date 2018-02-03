﻿using System.Collections.Generic;
using Poltorachka.Domain.Events;

namespace Poltorachka.Domain
{
    public abstract class DomainEntity
    {
        protected DomainEntity()
        {
            Events = new List<DomainEvent>();
        }

        public ICollection<DomainEvent> Events { get; }
    }
}
