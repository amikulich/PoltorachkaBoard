using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IFactsDashboardService
    {
        ICollection<FactDashboardViewModel> GetAll();
    }

    public class FactsDashboardService : IFactsDashboardService
    {
        private readonly IFactsQuery _factsQuery;

        public FactsDashboardService(IFactsQuery factsQuery)
        {
            _factsQuery = factsQuery;
        }

        public ICollection<FactDashboardViewModel> GetAll()
        {
            var facts = _factsQuery.Execute();

            return facts.Select(f => new FactDashboardViewModel()
            {
                WitnessName = f.WitnessName,
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
    }
}
