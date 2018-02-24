using Poltorachka.Domain;

namespace Poltorachka.DataAccess
{
    public class UserRemainingBalanceQuery : IUserRemainingBalanceQuery
    {
        public byte Execute(int indId)
        {
            return 4;
        }
    }
}
