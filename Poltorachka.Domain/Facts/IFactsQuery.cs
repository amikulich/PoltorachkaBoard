using System.Collections.Generic;

namespace Poltorachka.Domain.Facts
{
    public interface IFactsQuery
    {
        ICollection<FactReadModel> Execute();
    }
}
