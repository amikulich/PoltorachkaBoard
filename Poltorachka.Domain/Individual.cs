namespace Poltorachka.Domain
{
    public class Individual
    {
        public Individual(int individualId, string name)
        {
            IndividualId = individualId;
            Name = name;
        }

        public int IndividualId { get; }

        public string Name { get; }
    }
}
