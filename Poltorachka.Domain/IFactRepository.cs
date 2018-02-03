using System.Collections.Generic;

namespace Poltorachka.Domain
{
    public interface IFactRepository
    {
        IEnumerable<FactReadModel> Get();

        void Save(Fact fact);
    }
}
