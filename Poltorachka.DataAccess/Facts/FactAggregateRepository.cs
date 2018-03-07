using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain.Facts;

namespace Poltorachka.DataAccess.Facts
{
    public class FactAggregateRepository : IFactAggregateRepository
    {
        private readonly IConfiguration _configuration;

        public FactAggregateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Save(FactBase fact)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                if (fact.FactId == 0)
                {
                    fact.FactId = conn.Query<int>(@"
                        INSERT INTO [dbo].[fact] ([app_id], [winner_id], [loser_id], [creator_id], [approver_id], [status], [score], [description], [date])
                        VALUES (@appId, @winnerId, @loserId, @creatorId, @approverId, @status, @score, @description, @date);

                        SELECT SCOPE_IDENTITY();
                    ",
                                 new
                                     {
                                         appId = fact.AppId,
                                         winnerId = fact.WinnerId,
                                         loserId = fact.LoserId,
                                         creatorId = fact.CreatorId,
                                         approverId = fact.ApproverId,
                                         status = fact.Status,
                                         score = fact.Score,
                                         date = fact.Date,
                                         description = fact.Description,
                                     }).Single();
                }
                else
                {
                    conn.Execute(@"
                        UPDATE [dbo].[fact]
                        SET approver_id = @approverId
                            ,status = @status
                        WHERE fact_id = @factId
                    ",
                    new
                        {
                            approverId = fact.ApproverId,
                            status = fact.Status,
                            factId = fact.FactId,
                    });
                }

            }
        }

        public IFact Get(int factId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var fact = conn.Query($@"
                        SELECT [fact_id], 
                                [app_id],
                                [winner_id], 
                                [loser_id], 
                                [creator_id], 
                                [approver_id], 
                                [status], 
                                [score], 
                                [date], 
                                [description] 
                        FROM [dbo].[Fact]
                        WHERE fact_id = {factId}")
                    .Select(f => new Charge
                    {
                        FactId = f.fact_id,
                        AppId = f.app_id,
                        Date = f.date,
                        LoserId = f.loser_id,
                        WinnerId = f.winner_id,
                        ApproverId = f.approver_id,
                        CreatorId = f.creator_id,
                        Status = (FactStatus)f.status,
                        Score = (byte)f.score,
                        Description = f.description
                    }).Single();

                DateTime.SpecifyKind(fact.Date, DateTimeKind.Utc);

                return fact;
            }
        }

    }
}
