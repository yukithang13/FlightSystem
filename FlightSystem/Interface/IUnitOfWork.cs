namespace FlightSystem.Interface
{
    public interface IUnitOfWork
    {

        IAccountService AccountService { get; }
        IFlightService FlightService { get; }



        Task<bool> Complete();
        bool HasChanges();

    }
}
