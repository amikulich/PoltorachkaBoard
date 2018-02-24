using System.Collections.Generic;

namespace Poltorachka.Domain
{
    public interface IFactsQuery
    {
        ICollection<FactReadModel> Execute();
    }
}
