﻿using System.Collections.Generic;
using System.Linq;
using Poltorachka.Domain.Facts;
using Poltorachka.Web.Models;
using Poltorachka.Web.Pages.Facts;

namespace Poltorachka.Web.Services
{
    public interface IDashboardAppService
    {
        ICollection<FactDashboardViewModel> GetAll();
    }

    public class DashboardAppService : IDashboardAppService
    {
        private readonly IFactsQuery _factsQuery;

        public DashboardAppService(IFactsQuery factsQuery)
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
                Type = (FactTypeModelEnum)f.Type,
                Score = f.Score,
                Status = (FactStatusViewModel) f.Status
            }).ToList();
        }
    }
}
