using System.Collections.Generic;

namespace Poltorachka.Domain
{
    public interface IFactRepository
    {
        IEnumerable<Fact> Get();

        void Save(Fact fact);

        Fact GetById(int factId);
    }
}
