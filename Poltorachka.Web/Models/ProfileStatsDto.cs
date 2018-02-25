namespace Poltorachka.Web.Models
{
    public class ProfileStatsDto
    {
        public ProfileStatsDto(int monthlyScore, 
            int monthlyDonatesLeft, 
            int monthlyPeopleReached, 
            int overallScore, 
            int overallPosition)
        {
            MonthlyScore = monthlyScore;
            MonthlyDonatesLeft = monthlyDonatesLeft;
            MonthlyPeopleReached = monthlyPeopleReached;
            OverallScore = overallScore;
            OverallPosition = overallPosition;
        }

        public int MonthlyScore { get; }

        public int MonthlyDonatesLeft { get; }

        public int MonthlyPeopleReached { get; }

        public int OverallScore { get; }

        public int OverallPosition { get; }

    }
}
