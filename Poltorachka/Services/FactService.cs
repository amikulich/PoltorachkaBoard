using System;
using System.Linq;
using Poltorachka.Domain;

namespace Poltorachka.Services
{
    public interface IFactService
    {
        void Create(string winnerName,
            string loserName,
            byte score,
            string description,
            string userName);
    }

    public class FactService : IFactService
    {
        private readonly IFactRepository factRepository;
        private readonly IIndividualsQuery individualsQuery;

        public FactService(IFactRepository factRepository, IIndividualsQuery individualsQuery)
        {
            this.factRepository = factRepository;
            this.individualsQuery = individualsQuery;
        }

        public void Create(string winnerName, string loserName, byte score, string description, string userName)
        {
            var creatorName = individualsQuery.Execute().Single(u => u.Name == userName).Name;

            factRepository.Save(new Fact(winnerName, loserName, creatorName, score, description));
        }
    }
}
