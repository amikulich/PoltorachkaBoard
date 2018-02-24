namespace Poltorachka.Domain.Individuals
{
    public class Individual
    {
        public Individual(int indId, string name)
        {
            Assert.That(indId > 0, "[ind_id] should be > 0");
            Assert.NotNullOrEmpty(name, nameof(name));

            IndId = indId;
            Name = name;
        }

        public int IndId { get; }

        public string Name { get; }
    }
}
