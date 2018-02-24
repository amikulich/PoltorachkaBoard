﻿using System;
using System.Linq;
using Poltorachka.Domain;
using Poltorachka.Domain.Facts;
using Poltorachka.Domain.Individuals;
using Poltorachka.Models;

namespace Poltorachka.Services
{
    public interface IFactAppService
    {
        void Create(int winnerId, int loserId, Guid userId, byte score, string description);

        FactEditViewModel Get(int factId);

        void Update(int factId, Guid userId, FactStatusViewModel status);
    }

    public class FactAppService : IFactAppService
    {
        private readonly IFactAggregateRepository _factAggregateRepository;
        private readonly IUserRemainingBalanceQuery _userRemainingBalanceQuery;
        private readonly IIndividualsQuery _individualsQuery;
        private readonly IFactsQuery _factsQuery;

        public FactAppService(IFactAggregateRepository factAggregateRepository, 
            IUserRemainingBalanceQuery userRemainingBalanceQuery,
            IIndividualsQuery individualsQuery,
            IFactsQuery factsQuery)
        {
            _factAggregateRepository = factAggregateRepository;
            _userRemainingBalanceQuery = userRemainingBalanceQuery;
            _individualsQuery = individualsQuery;
            _factsQuery = factsQuery;
        }

        public void Create(int winnerId, int loserId, Guid userId, byte score, string description)
        {
            try
            {
                var fact = new Fact(winnerId, 
                    loserId,
                    _individualsQuery.Execute(userId).IndId,
                    score, 
                    description,
                    _userRemainingBalanceQuery.Execute);

                _factAggregateRepository.Save(fact);
            }
            catch (DomainAssertException exception)
            {
                throw new AppServiceException(exception.Message, exception);
            }
        }

        public FactEditViewModel Get(int factId)
        {
            var fact = _factsQuery.Execute().Single(f => f.FactId == factId);

            return new FactEditViewModel()
            {
                CreatorId = fact.CreatorId,
                CreatorName = fact.CreatorName,
                LoserId = fact.LoserId,
                LoserName = fact.LoserName,
                WinnerId = fact.WinnerId,
                WinnerName = fact.WinnerName,
                WitnessId = fact.WitnessId,
                WitnessName = fact.WitnessName,
                Score = fact.Score,
                Date = fact.Date,
                Description = fact.Description,
                FactId = fact.FactId
            };
        }

        public void Update(int factId, Guid userId, FactStatusViewModel status)
        {
            try
            {
                var fact = _factAggregateRepository.Get(factId);
                var individualId = _individualsQuery.Execute(userId).IndId;

                switch (status)
                {
                    case FactStatusViewModel.Approved:
                        fact.Approve(individualId);
                        break;
                    case FactStatusViewModel.Canceled:
                        fact.Decline(individualId);
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                _factAggregateRepository.Save(fact);
            }
            catch (DomainAssertException exception)
            {
                throw new AppServiceException(exception.Message, exception);
            }
        }
    }
}
