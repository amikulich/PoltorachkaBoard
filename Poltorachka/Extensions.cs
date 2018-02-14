using System.Linq;
using System.Security.Claims;

namespace Poltorachka
{
    public static class Extensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.Single(c => c.Type == "name").Value;
        }
    }
}