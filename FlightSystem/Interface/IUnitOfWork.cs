namespace FlightSystem.Interface
{
    public interface IUnitOfWork
    {

        IFlightService FlightService { get; }

        IDocumentInfoService DocumentInfoService { get; }

        IGroupService GroupService { get; }

        IGroupRoleService GroupRoleService { get; }

        IGroupInfoService GroupInfoService { get; }



        Task<bool> Complete();
        bool HasChanges();

    }
}
