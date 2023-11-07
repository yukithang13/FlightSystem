using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Interface;

namespace FlightSystem.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;
        public UnitOfWork(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            _mapper = mapper;
            _dbcontext = dbcontext;
        }
        public IFlightService FlightService => new FlightService(_dbcontext, _mapper);



        public IAccountService AccountService => throw new NotImplementedException();



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
