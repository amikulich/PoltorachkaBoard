using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain;

namespace Poltorachka.DataAccess
{
    public class FactsQuery : IFactsQuery
    {
        private readonly IConfiguration _configuration;

        private const string Sql = @"
                        ;WITH individuals ([ind_id], [name])
                        AS
                        (
                            SELECT ind_id
                                ,name
                            FROM [dbo].[individual]
                        )
                        SELECT TOP 1000 [fact_id]
                              ,f.[loser_id]
                              ,loser.name AS loser_name
                              ,f.winner_id
                              ,winner.name AS winner_name
                              ,f.creator_id
                              ,creator.name AS creator_name
                              ,f.approver_id
                              ,approver.name AS approver_name
                              ,f.[status]
                              ,f.[score]
                              ,f.[date]
                              ,f.[description]
                        FROM [dbo].[fact] f
                        OUTER APPLY
                        (
                            SELECT TOP 1 i.name
                            FROM individuals i
                            WHERE f.winner_id = i.ind_id
                        ) winner
                        OUTER APPLY
                        (
                            SELECT TOP 1 i.name
                            FROM individuals i
                            WHERE f.loser_id = i.ind_id
                        ) loser
                        OUTER APPLY
                        (
                            SELECT TOP 1 i.name
                            FROM individuals i
                            WHERE f.approver_id = i.ind_id
                        ) approver
                        OUTER APPLY
                        (
                            SELECT TOP 1 i.name
                            FROM individuals i
                            WHERE f.creator_id = i.ind_id
                        ) creator";

        public FactsQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICollection<FactReadModel> Execute()
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var facts = conn.Query(Sql)
                    .Select(f => new FactReadModel()
                    { FactId = f.fact_id,
                        WinnerId = f.winner_id,
                        WinnerName = f.winner_name,
                        LoserId = f.loser_id,
                        LoserName = f.loser_name,
                        CreatorId = f.creator_id,
                        CreatorName = f.creator_name,
                        WitnessId = f.approver_id,
                        WitnessName = f.approver_name,
                        Score = (byte)f.score,
                        Description = f.description,
                        Date = f.date,
                        Status = (FactStatus)f.status}).ToList();

                facts.ForEach(fact => DateTime.SpecifyKind(fact.Date, DateTimeKind.Utc));

                return facts;
            }
        }
    }
}
