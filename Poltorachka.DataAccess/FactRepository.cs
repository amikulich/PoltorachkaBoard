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

        public IEnumerable<FactReadModel> Get()
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var facts = conn.Query("SELECT loser_id,[date],winner_id,score FROM [dbo].[Fact]")
                    .Select(f => new FactMap
                    {
                        Date = f.date,
                        LoserId = f.loser_id,
                        WinnerId = f.winner_id,
                        Score = f.score
                    }).ToList();

                facts.ForEach(f => DateTime.SpecifyKind(f.Date, DateTimeKind.Utc));

                var names = conn.Query("SELECT [ind_id], [name] FROM [dbo].[individual]")
                    .Select(u => new IndividualMap()
                    {
                        IndividualId = u.ind_id,
                        IndividualName = u.name
                    }).ToList();

                return facts.Select(f => new FactReadModel()
                {
                    Date = f.Date,
                    LoserName = names.Single(n => n.IndividualId == f.LoserId).IndividualName,
                    Score = (byte) f.Score,
                    WinnerName = names.Single(n => n.IndividualId == f.WinnerId).IndividualName
                }).ToList();
            }
        }

        public void Save(Fact fact)
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                conn.Execute(@"
                        DECLARE @WinnerId INT, @FactId INT, @LoserId INT;

                        SELECT @WinnerId = ind_id FROM [dbo].[individual] WHERE name = @WinnerName
                        SELECT @LoserId = ind_id FROM [dbo].[individual] WHERE name = @LoserName

                        INSERT INTO [dbo].[fact] ([winner_id], [loser_id], [score], [date])
                        VALUES (@WinnerId, @LoserId, @Score, @Date);

                        SET @FactId = SCOPE_IDENTITY();
                    ",
                    new
                    {
                        WinnerName = fact.WinnerName,
                        LoserName = fact.LoserName,
                        Score = fact.Score,
                        Date = fact.Date
                    });
            }
        }

        private class FactMap
        {
            public int WinnerId { get; set; }

            public int LoserId { get; set; }

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
