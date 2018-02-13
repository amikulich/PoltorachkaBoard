namespace Poltorachka.Domain
{
    public class Individual
    {
        public Individual(int individualId, string name, string userName)
        {
            IndividualId = individualId;
            Name = name;
            UserName = userName;
        }

        public Individual(int individualId, string name)
        {
            IndividualId = individualId;
            Name = name;
            UserName = null;
        }

        public int IndividualId { get; }

        public string Name { get; }

        public string UserName { get; }
    }
}
