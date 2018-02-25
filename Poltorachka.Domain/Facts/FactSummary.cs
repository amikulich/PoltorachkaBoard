using System;
using System.Collections.Generic;

namespace Poltorachka.Domain.Facts
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
        public UserSummary(string individualName, int score, int position)
        {
            IndividualName = individualName;
            Score = score;
            Position = position;
        }

        public string IndividualName { get; private set; }

        public int Score { get; private set; }

        public int Position { get; private set; }
    }
}
