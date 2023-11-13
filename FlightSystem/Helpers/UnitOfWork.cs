using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Services;

namespace FlightSystem.Helpers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UnitOfWork(FlightSystemDBContext dbcontext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _mapper = mapper;
            _dbcontext = dbcontext;
            _hostEnvironment = hostEnvironment;
        }

        public IFlightService FlightService => new FlightService(_dbcontext, _mapper);

        public IDocumentInfoService DocumentInfoService => new DocumentInfoService(_dbcontext, _mapper, _hostEnvironment);

        public IGroupService GroupService => new GroupService(_dbcontext, _mapper);






        public async Task<bool> Complete()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _dbcontext.ChangeTracker.HasChanges();
        }
    }
}
