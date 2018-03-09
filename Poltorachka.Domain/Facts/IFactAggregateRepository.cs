namespace Poltorachka.Domain.Facts
{
    public interface IFactAggregateRepository
    {
        void Save(FactBase fact);

        FactBase Get(int factId);
    }
}
