using System;
using System.Collections.Generic;

namespace Poltorachka.Domain
{
    public class FactSummary
    {
        public FactSummary(ICollection<UserSummary> users, 
            DateTime startDate, 
            DateTime endDate)
        {
            Users = users;
            StartDate = startDate;
            EndDate = endDate;
        }

        public ICollection<UserSummary> Users { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }
    }

    public class UserSummary
    {
        public UserSummary(string userName, int score)
        {
            UserName = userName;
            Score = score;
        }

        public string UserName { get; private set; }

        public int Score { get; private set; }
    }
}
