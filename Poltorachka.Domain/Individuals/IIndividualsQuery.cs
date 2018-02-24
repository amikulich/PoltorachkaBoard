using System;
using System.Collections.Generic;

namespace Poltorachka.Domain.Individuals
{
    public interface IIndividualsQuery
    {
        ICollection<Individual> Execute();

        Individual Execute(Guid userId);
    }
}
