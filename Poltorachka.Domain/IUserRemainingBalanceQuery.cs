namespace Poltorachka.Domain
{
    public interface IUserRemainingBalanceQuery
    {
        byte Execute(int indId);
    }
}