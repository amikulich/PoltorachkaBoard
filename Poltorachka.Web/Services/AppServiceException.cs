using System;

namespace Poltorachka.Web.Services
{
    public class AppServiceException : Exception
    {
        public AppServiceException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
