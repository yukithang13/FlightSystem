namespace FlightSystem.Interface
{
    public interface IUnitOfWork
    {

        IAccountService AccountService { get; }
        IFlightService FlightService { get; }

        IDocumentInfoService DocumentInfoService { get; }



        Task<bool> Complete();
        bool HasChanges();

    }
}
