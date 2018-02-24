using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain;
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

        public void Save(Fact fact)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                if (fact.FactId == 0)
                {
                    fact.FactId = conn.Query<int>(@"
                        INSERT INTO [dbo].[fact] ([winner_id], [loser_id], [creator_id], [approver_id], [status], [score], [description], [date])
                        VALUES (@WinnerId, @LoserId, @CreatorId, @ApproverId, @Status, @Score, @Description, @Date);
                    ",
                                 new
                                     {
                                         WinnerId = fact.WinnerId,
                                         LoserId = fact.LoserId,
                                         CreatorId = fact.CreatorId,
                                         ApproverId = fact.ApproverId,
                                         Status = fact.Status,
                                         Score = fact.Score,
                                         Date = fact.Date,
                                         Description = fact.Description,
                                     }).Single();
                }
                else
                {
                    conn.Execute(@"
                        UPDATE [dbo].[fact]
                        SET approver_id = @ApproverId
                            ,status = @Status
                        WHERE fact_id = @FactId
                    ",
                    new
                        {
                            ApproverId = fact.ApproverId,
                            Status = fact.Status,
                            FactId = fact.FactId,
                    });
                }

            }
        }

        public Fact Get(int factId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var fact = conn.Query($@"
                        SELECT [fact_id], 
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
                    .Select(f => new Fact
                    {
                        FactId = f.fact_id,
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
