namespace Poltorachka.Domain
{
    public class Individual
    {
        public Individual(int individualId, string name, string email)
        {
            IndividualId = individualId;
            Name = name;
            Email = email;
        }

        public int IndividualId { get; }

        public string Name { get; }

        public string Email { get; }
    }
}
