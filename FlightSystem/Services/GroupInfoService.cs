using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Services
{
    public class GroupInfoService : IGroupInfoService
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;

        public GroupInfoService(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        //get all
        public async Task<List<GroupInfo>> GetAllGroupInfoByAsync()
        {
            var grs = await _dbcontext.GroupInfo.ToListAsync();
            return _mapper.Map<List<GroupInfo>>(grs);
        }

        // get 1 id
        public async Task<GroupInfo> GetGroupInfoByIdAsync(int id)
        {
            var gr = await _dbcontext.GroupInfo.FindAsync(id);
            return _mapper.Map<GroupInfo>(gr);
        }
        // add
        public async Task<int> AddGroupInfoAsync(GroupInfoModel groupinfomodel)
        {
            var newG = _mapper.Map<GroupInfo>(groupinfomodel);
            _dbcontext.GroupInfo.Add(newG);
            await _dbcontext.SaveChangesAsync();

            return newG.GroupId;
        }

        //update
        public async Task UpdateGroupInfoAsync(int id, GroupInfoModel groupinfomodel)
        {
            var existingGroupInfo = await _dbcontext.GroupInfo.FindAsync(groupinfomodel);

            if (existingGroupInfo == null)
            {
                throw new Exception("Group not found");
            }
            if (id == groupinfomodel.GroupInfoId)
            {
                var updateGr = _mapper.Map<GroupInfo>(groupinfomodel);
                _dbcontext.GroupInfo.Update(updateGr);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteGroupInfoAsync(int id)
        {
            var deleteG = _dbcontext.GroupInfo.SingleOrDefault(g => g.GroupInfoId == id);
            if (deleteG != null)
            {
                _dbcontext.GroupInfo.Remove(deleteG);
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
