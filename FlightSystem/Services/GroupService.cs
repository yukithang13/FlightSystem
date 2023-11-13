using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Services
{
    public class GroupService : IGroupService
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;

        public GroupService(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        //get all
        public async Task<List<GroupModel>> GetAllGroupByAsync()
        {
            var grs = await _dbcontext.Groups.ToListAsync();
            return _mapper.Map<List<GroupModel>>(grs);
        }

        // get 1 id
        public async Task<GroupModel> GetGroupByIdAsync(int id)
        {
            var gr = await _dbcontext.Groups.FindAsync(id);
            return _mapper.Map<GroupModel>(gr);
        }
        // add
        public async Task<int> AddGroupAsync(GroupModel groupmodel)
        {
            var newG = _mapper.Map<Group>(groupmodel);
            _dbcontext.Groups.Add(newG);
            await _dbcontext.SaveChangesAsync();

            return newG.GroupId;
        }

        //update
        public async Task UpdateGroupAsync(int id, GroupModel groupmodel)
        {
            var existingGroup = await _dbcontext.Groups.FindAsync(groupmodel);

            if (existingGroup == null)
            {
                throw new Exception("Product not found");
            }
            if (id == groupmodel.GroupId)
            {
                var updateG = _mapper.Map<Group>(groupmodel);
                _dbcontext.Groups.Update(updateG);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteGroupAsync(int id)
        {
            var deleteG = _dbcontext.Groups.SingleOrDefault(g => g.GroupId == id);
            if (deleteG != null)
            {
                _dbcontext.Groups.Remove(deleteG);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // find time
        public async Task<List<Group>> SearchGroupsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbcontext.Groups
                .Where(d => d.CreatedAt >= startDate && d.CreatedAt <= endDate)
                .ToListAsync();
        }
        // Find name, note
        public async Task<List<Group>> SearchGroupsAsync(string searchTerm)
        {
            return await _dbcontext.Groups
                .Where(d => d.GroupName.Contains(searchTerm) || d.Note.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
