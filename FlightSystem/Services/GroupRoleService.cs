using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Services
{
    public class GroupRoleService : IGroupRoleService
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;

        public GroupRoleService(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        //get all
        public async Task<List<GroupRole>> GetAllGroupRoleByAsync()
        {
            var grs = await _dbcontext.GroupRoles.ToListAsync();
            return _mapper.Map<List<GroupRole>>(grs);
        }

        // get 1 id
        public async Task<GroupRole> GetGroupRoleByIdAsync(int id)
        {
            var gr = await _dbcontext.GroupRoles.FindAsync(id);
            return _mapper.Map<GroupRole>(gr);
        }
        // add
        public async Task<int> AddGroupRoleAsync(GroupRoleModel grouprolemodel)
        {
            var newG = _mapper.Map<GroupRole>(grouprolemodel);
            _dbcontext.GroupRoles.Add(newG);
            await _dbcontext.SaveChangesAsync();

            return newG.GroupId;
        }

        //update
        public async Task UpdateGroupRoleAsync(int id, GroupRoleModel grouprolemodel)
        {
            var existingGroupRole = await _dbcontext.GroupRoles.FindAsync(grouprolemodel);

            if (existingGroupRole == null)
            {
                throw new Exception("Group not found");
            }
            if (id == grouprolemodel.GroupRoleId)
            {
                var updateGr = _mapper.Map<GroupRole>(grouprolemodel);
                _dbcontext.GroupRoles.Update(updateGr);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteGroupRoleAsync(int id)
        {
            var deleteG = _dbcontext.GroupRoles.SingleOrDefault(g => g.GroupRoleId == id);
            if (deleteG != null)
            {
                _dbcontext.GroupRoles.Remove(deleteG);
                await _dbcontext.SaveChangesAsync();
            }
        }


    }
}
