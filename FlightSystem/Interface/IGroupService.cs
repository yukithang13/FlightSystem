using FlightSystem.Data;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IGroupService
    {
        Task<int> AddGroupAsync(GroupModel groupmodel);
        Task DeleteGroupAsync(int id);
        Task<List<GroupModel>> GetAllGroupByAsync();
        Task<GroupModel> GetGroupByIdAsync(int id);
        Task<List<Group>> SearchGroupsAsync(string searchTerm);
        Task<List<Group>> SearchGroupsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task UpdateGroupAsync(int id, GroupModel groupmodel);
    }
}
