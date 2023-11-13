using FlightSystem.Data;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IGroupRoleService
    {
        Task<int> AddGroupRoleAsync(GroupRoleModel grouprolemodel);
        Task DeleteGroupRoleAsync(int id);
        Task<List<GroupRole>> GetAllGroupRoleByAsync();
        Task<GroupRole> GetGroupRoleByIdAsync(int id);
        Task UpdateGroupRoleAsync(int id, GroupRoleModel grouprolemodel);
    }
}
