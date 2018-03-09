using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain.Facts;

namespace Poltorachka.DataAccess.FactsSummary
{
    public class FactSummaryQuery : IFactSummaryQuery
    {
        private const string Sql = @"
          ;WITH winners (ind_id, score)
          AS
          (
             SELECT winner_id, SUM(score) 
             FROM fact f
             WHERE f.status = 2 /*Approved*/
                AND f.date BETWEEN @startDate AND @endDate
             GROUP BY winner_id
          )
          ,losers (ind_id, score)
          AS
          (
             SELECT loser_id, SUM(score) 
             FROM fact f
             WHERE f.status = 2 /*Approved*/
                AND f.fact_type_id = 1 /* Charge */
                AND f.date BETWEEN @startDate AND @endDate
             GROUP BY loser_id 
          )
          ,results (ind_id, score)
          AS
          (
            SELECT COALESCE(w.ind_id, l.ind_id) AS ind_id
                ,COALESCE(w.score, 0) - COALESCE(l.score, 0) AS score
            FROM winners w
            FULL OUTER JOIN losers l ON l.ind_id = w.ind_id
          )
          SELECT ind_id AS IndividualId
                    ,score AS Score
                    ,ROW_NUMBER() OVER (ORDER BY score DESC) AS Position
          FROM results
        ";

        private readonly IConfiguration configuration;

        public FactSummaryQuery(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FactSummary Execute(DateTime startDate, DateTime endDate)
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var userScores = conn.Query<UserSummaryMap>(Sql, new { startDate, endDate});

                var names = conn.Query("SELECT * FROM [dbo].[individual]")
                    .Select(u => new IndividualMap()
                    {
                        IndividualId = u.ind_id,
                        IndividualName = u.name
                    }).ToList();

                var userSummaries = userScores
                    .Select(u =>
                        new UserSummary(names.Single(n => n.IndividualId == u.IndividualId).IndividualName, u.Score, u.Position))
                    .ToList();

                return new FactSummary(userSummaries, startDate, endDate);
            }
        }

        public FactSummary Execute()
        {
            var startDate = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = new DateTime(9999, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Execute(startDate, endDate);
        }

        private class UserSummaryMap
        {
            public int IndividualId { get; set; }

            public int Score { get; set; }

            public int Position { get; set; }
        }

        private class IndividualMap
        {
            public int IndividualId { get; set; }

            public string IndividualName { get; set; }
        }
    }
}
