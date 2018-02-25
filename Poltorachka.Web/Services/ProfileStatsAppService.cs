using System;
using System.Linq;
using Poltorachka.Domain.Facts;
using Poltorachka.Domain.Individuals;
using Poltorachka.Web.Models;
using Poltorachka.Web.Shared;

namespace Poltorachka.Web.Services
{
    public interface IProfileStatsAppService
    {
        ProfileStatsDto Get(Guid userId);
    }

    public class ProfileStatsAppService : IProfileStatsAppService
    {
        private readonly IFactSummaryQuery _factSummaryQuery;
        private readonly IIndividualsQuery _individualsQuery;
        private readonly IUserRemainingBalanceQuery _userRemainingBalanceQuery;

        public ProfileStatsAppService(IFactSummaryQuery factSummaryQuery, IIndividualsQuery individualsQuery, IUserRemainingBalanceQuery userRemainingBalanceQuery)
        {
            _factSummaryQuery = factSummaryQuery;
            _individualsQuery = individualsQuery;
            _userRemainingBalanceQuery = userRemainingBalanceQuery;
        }

        public ProfileStatsDto Get(Guid userId)
        {
            var range = DateTime.UtcNow.AsMonthStartEndDates();
            var monthlyStats = _factSummaryQuery.Execute(range.Item1, range.Item2);
            var overallStats = _factSummaryQuery.Execute();

            var user = _individualsQuery.Execute(userId);

            var monthlyUserStats = monthlyStats.Users.SingleOrDefault(u => u.IndividualName == user.Name);
            monthlyUserStats = monthlyUserStats ?? new UserSummary(user.Name, 0, monthlyStats.Users.Count + 1);

            var overallUserStats = overallStats.Users.SingleOrDefault(u => u.IndividualName == user.Name);
            overallUserStats = overallUserStats ?? new UserSummary(user.Name, 0, overallStats.Users.Count + 1);

            var remainingBalance = _userRemainingBalanceQuery.Execute(user.IndId);

            return new ProfileStatsDto(monthlyUserStats.Score,
                remainingBalance,
                0,
                overallUserStats.Score,
                overallUserStats.Position);
        }
    }
}
