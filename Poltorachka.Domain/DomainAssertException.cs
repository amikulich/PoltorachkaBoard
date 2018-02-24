using System;

namespace Poltorachka.Domain
{
    public class DomainAssertException : Exception
    {
        public DomainAssertException(string message) : base(message)
        {
        }
    }
}
