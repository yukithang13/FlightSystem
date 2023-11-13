using FlightSystem.Data;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IGroupInfoService
    {
        Task<int> AddGroupInfoAsync(GroupInfoModel groupinfomodel);
        Task DeleteGroupInfoAsync(int id);
        Task<List<GroupInfo>> GetAllGroupInfoByAsync();
        Task<GroupInfo> GetGroupInfoByIdAsync(int id);
        Task UpdateGroupInfoAsync(int id, GroupInfoModel groupinfomodel);
    }
}
