using System;

namespace Poltorachka.Domain.Facts
{
    public interface IFactSummaryQuery
    {
        FactSummary Execute();

        FactSummary Execute(DateTime from, DateTime to);
    }
}
