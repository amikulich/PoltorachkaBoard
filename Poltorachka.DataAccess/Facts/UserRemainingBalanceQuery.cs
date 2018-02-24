using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Poltorachka.Domain.Facts;

namespace Poltorachka.DataAccess.Facts
{
    public class UserRemainingBalanceQuery : IUserRemainingBalanceQuery
    {
        private readonly IConfiguration _configuration;

        private const string Sql = @"
            SELECT @limit - ISNULL(SUM(f.score), 0)
            FROM fact f 
            WHERE f.loser_id = @indId
                AND f.[date] BETWEEN @startDate AND @endDate
                AND f.[status] IN (1 /*Pending*/, 2 /*Approved*/)
        ";

        public UserRemainingBalanceQuery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte Execute(int indId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("MainDb")))
            {
                conn.Open();

                var today = DateTime.UtcNow;

                var potlorachkasLeft = conn.Query<int>(Sql, 
                    new
                    {
                        indId,
                        limit = 4,
                        startDate = today.AddDays(-30),
                        endDate = today
                    })
                    .Single();

                return (byte) (potlorachkasLeft > 0 ? potlorachkasLeft : 0);
            }
        }
    }
}
