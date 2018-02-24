namespace Poltorachka.Domain.Facts
{
    public interface IUserRemainingBalanceQuery
    {
        byte Execute(int indId);
    }
}