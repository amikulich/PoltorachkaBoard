using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain.Individuals;

namespace Poltorachka.DataAccess.Individuals
{
    public class IndividualsQuery : IIndividualsQuery
    {
        private readonly IConfiguration _configuration;

        private const string Sql = @"
            SELECT i.[name]
                  ,i.[ind_id]
            FROM [dbo].[individual] i
            JOIN [dbo].[AspNetUsers] r ON r.Id = i.[user_id]";

        public IndividualsQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICollection<Individual> Execute()
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                return conn.Query(Sql).Select(i => new Individual(i.ind_id, i.name))
                    .ToList();
            }
        }

        public Individual Execute(Guid userId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                return conn.Query(Sql + " WHERE [user_id] = @userId", new { userId })
                    .Select(i => new Individual(i.ind_id, i.name))
                    .Single();
            }
        }
    }
}
