using System;
using System.Linq;
using Poltorachka.Domain.Facts;

namespace Poltorachka.Web.Authorization
{
    public interface IFactAccessService
    {
        void ValidateFactExists(int factId, Guid userId);
    }

    public class FactAccessService : IFactAccessService
    {
        private readonly IFactsQuery _factsQuery;

        public FactAccessService(IFactsQuery factsQuery)
        {
            _factsQuery = factsQuery;
        }

        public void ValidateFactExists(int factId, Guid userId)
        {
            var fact = _factsQuery.Execute().SingleOrDefault(f => f.FactId == factId);

            if (fact == null)
            {
                throw new PageNotFoundException();
            }
        }
    }
}
