namespace Poltorachka.Domain
{
    public interface IFactAggregateRepository
    {
        void Save(Fact fact);

        Fact Get(int factId);
    }
}
