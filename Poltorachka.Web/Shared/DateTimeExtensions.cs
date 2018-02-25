using System;

namespace Poltorachka.Web.Shared
{
    public static class DateTimeExtensions
    {
        public static Tuple<DateTime, DateTime> AsMonthStartEndDates(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Should be UTC datetime");
            }

            var startDate = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1).AddMilliseconds(-1);

            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
