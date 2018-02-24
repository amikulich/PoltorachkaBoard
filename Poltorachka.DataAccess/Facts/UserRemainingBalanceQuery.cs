using Poltorachka.Domain.Facts;

namespace Poltorachka.DataAccess.Facts
{
    public class UserRemainingBalanceQuery : IUserRemainingBalanceQuery
    {
        public byte Execute(int indId)
        {
            return 4;
        }
    }
}
