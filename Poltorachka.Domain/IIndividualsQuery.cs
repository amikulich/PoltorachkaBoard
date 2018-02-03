using System.Collections.Generic;

namespace Poltorachka.Domain
{
    public interface IIndividualsQuery
    {
        ICollection<Individual> Execute();
    }
}
