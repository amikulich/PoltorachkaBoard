using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain;

namespace Poltorachka.DataAccess
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
             GROUP BY winner_id
          )
          ,losers (ind_id, score)
          AS
          (
             SELECT loser_id, SUM(score) 
             FROM fact f
             WHERE f.status = 2 /*Approved*/
             GROUP BY loser_id  
          )
          SELECT COALESCE(w.ind_id, l.ind_id) AS IndividualId
            ,COALESCE(w.score, 0) - COALESCE(l.score, 0) AS Score
          FROM winners w
          FULL OUTER JOIN losers l ON l.ind_id = w.ind_id
        ";

        private readonly IConfiguration configuration;

        public FactSummaryQuery(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public FactSummary Execute()
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var userScores = conn.Query<UserSummaryMap>(Sql);

                var names = conn.Query("SELECT * FROM [dbo].[individual]")
                    .Select(u => new IndividualMap()
                    {
                        IndividualId = u.ind_id,
                        IndividualName = u.name
                    }).ToList();

                var userSummaries = userScores
                    .Select(u =>
                        new UserSummary(names.Single(n => n.IndividualId == u.IndividualId).IndividualName, u.Score))
                    .ToList();

                return new FactSummary(userSummaries, DateTime.MinValue, DateTime.MaxValue);
            }
        }

        private class UserSummaryMap
        {
            public int IndividualId { get; set; }

            public int Score { get; set; }
        }

        private class IndividualMap
        {
            public int IndividualId { get; set; }

            public string IndividualName { get; set; }
        }
    }
}
