using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain;

namespace Poltorachka.DataAccess
{
    public class IndividualsQuery : IIndividualsQuery
    {
        private readonly IConfiguration configuration;

        private const string Sql = @"
            SELECT i.[ind_id]
                  ,i.[name]
                  ,r.[UserName]
            FROM [dbo].[individual] i
            LEFT JOIN [dbo].[AspNetUsers] r ON r.Id = i.[user_id]";

        public IndividualsQuery(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ICollection<Individual> Execute()
        {
            using (var conn = new SqlConnection(configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                return conn.Query(Sql).Select(i => i.UserName != null ?
                    new Individual(i.ind_id, i.name, i.UserName)
                    : new Individual(i.ind_id, i.name))
                    .ToList();
            }
        }
    }
}
