using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Poltorachka.Domain;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Poltorachka.DataAccess
{
    public class FactRepository : IFactRepository
    {
        private readonly IConfiguration configuration;

        public FactRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Fact> Get()
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var facts = conn.Query("SELECT [fact_id], [winner_id], [loser_id], [creator_id], [approver_id], [status], [score], [date] FROM [dbo].[Fact]")
                    .Select(f => new FactMap
                    {
                        FactId = f.fact_id,
                        Date = f.date,
                        LoserId = f.loser_id,
                        WinnerId = f.winner_id,
                        ApproverId = f.approver_id,
                        CreatorId = f.creator_id,
                        Status = f.status,
                        Score = f.score
                    }).ToList();

                facts.ForEach(f => DateTime.SpecifyKind(f.Date, DateTimeKind.Utc));

                var names = conn.Query("SELECT [ind_id], [name] FROM [dbo].[individual]")
                    .Select(u => new IndividualMap()
                    {
                        IndividualId = u.ind_id,
                        IndividualName = u.name
                    }).ToList();

                return facts.Select(f => new Fact()
                {
                    FactId = f.FactId,
                    Date = f.Date,
                    LoserName = names.Single(n => n.IndividualId == f.LoserId).IndividualName,
                    Score = (byte) f.Score,
                    Status = (FactStatus)f.Status,
                    ApproverName = names.SingleOrDefault(n => n.IndividualId == f.ApproverId)?.IndividualName,
                    CreatorName = names.Single(n => n.IndividualId == f.CreatorId).IndividualName,
                    WinnerName = names.Single(n => n.IndividualId == f.WinnerId).IndividualName
                }).ToList();
            }
        }

        public void Save(Fact fact)
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                if (fact.FactId == 0)
                {
                    conn.Execute(@"
                        DECLARE @WinnerId INT, @FactId INT, @LoserId INT, @ApproverId INT, @CreatorId INT;

                        SELECT @WinnerId = ind_id FROM [dbo].[individual] WHERE name = @WinnerName
                        SELECT @LoserId = ind_id FROM [dbo].[individual] WHERE name = @LoserName
                        SELECT @ApproverId = ind_id FROM [dbo].[individual] WHERE name = @ApproverName
                        SELECT @CreatorId = ind_id FROM [dbo].[individual] WHERE name = @CreatorName

                        INSERT INTO [dbo].[fact] ([winner_id], [loser_id], [creator_id], [approver_id], [status], [score], [date])
                        VALUES (@WinnerId, @LoserId, @CreatorId, @ApproverId, @Status, @Score, @Date);

                        SET @FactId = SCOPE_IDENTITY();
                    ",
                                 new
                                     {
                                         WinnerName = fact.WinnerName,
                                         LoserName = fact.LoserName,
                                         CreatorName = fact.CreatorName,
                                         ApproverName = fact.ApproverName,
                                         Status = fact.Status,
                                         Score = fact.Score,
                                         Date = fact.Date
                                     });
                }
                else
                {
                    conn.Execute(@"
                        DECLARE @ApproverId INT;
                        SELECT @ApproverId = ind_id FROM [dbo].[individual] WHERE name = @ApproverName;

                        UPDATE [dbo].[fact]
                        SET approver_id = @ApproverId
                            ,status = @Status
                    ",
                    new
                        {
                            ApproverName = fact.ApproverName,
                            Status = fact.Status
                    });
                }

            }
        }

        public Fact GetById(int factId)
        {
            return Get().Single(f => f.FactId == factId);
        }

        private class FactMap
        {
            public int FactId { get; set; }

            public int WinnerId { get; set; }

            public int LoserId { get; set; }

            public int? ApproverId { get; set; }

            public int CreatorId { get; set; }

            public byte Status { get; set; }

            public int Score { get; set; }

            public DateTime Date { get; set; }
        }

        private class IndividualMap
        {
            public int IndividualId { get; set; }

            public string IndividualName { get; set; }
        }
    }
}
