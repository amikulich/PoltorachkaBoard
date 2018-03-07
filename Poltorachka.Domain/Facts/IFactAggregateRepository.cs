namespace Poltorachka.Domain.Facts
{
    public interface IFactAggregateRepository
    {
        void Save(FactBase fact);

        IFact Get(int factId);
    }
}
