namespace Poltorachka.Domain.Facts
{
    public interface IFactAggregateRepository
    {
        void Save(Fact fact);

        Fact Get(int factId);
    }
}
