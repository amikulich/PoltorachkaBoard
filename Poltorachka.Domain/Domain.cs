namespace Poltorachka.Domain
{
    public static class Domain
    {
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new DomainAssertException(message);
            }
        }
    }
}
