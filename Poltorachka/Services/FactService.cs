using System;
using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IFactsService
    {
        ICollection<FactViewModel> GetAll();

        void Create(CreateFactViewModel fact, Guid userId);
    }

    public class FactsService : IFactsService
    {
        private readonly IFactRepository factRepository;
        private readonly IIndividualsQuery individualsQuery;

        public FactsService(IFactRepository factRepository,
            IIndividualsQuery individualsQuery)
        {
            this.factRepository = factRepository;
            this.individualsQuery = individualsQuery;
        }

        public ICollection<FactViewModel> GetAll()
        {
            var facts = factRepository.Get();

            return facts.Select(f => new FactViewModel()
            {
                ApproverName = f.ApproverName,
                CreatorName = f.CreatorName,
                WinnerName = f.WinnerName,
                LoserName = f.LoserName,
                Description = f.Description,
                FactId = f.FactId,
                Date = f.Date,
                Score = f.Score,
                Status = (FactStatusViewModel) f.Status
            }).ToList();
        }

        public void Create(CreateFactViewModel fact, Guid userId)
        {
            var creatorName = individualsQuery.Execute().Where(u => u.);

            factRepository.Save(new Fact(fact.WinnerName, fact.LoserName, creatorName, fact.Score, fact.Description));
        }
    }
}
