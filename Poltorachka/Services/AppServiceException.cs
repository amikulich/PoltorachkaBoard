using System;

namespace Poltorachka.Services
{
    public class AppServiceException : Exception
    {
        public AppServiceException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
