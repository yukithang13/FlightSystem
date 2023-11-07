using AutoMapper;
using FlightSystem.Data;
using FlightSystem.Helpers;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightSystemDBContext _dbcontext;
        private readonly IMapper _mapper;

        public FlightService(FlightSystemDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        //get all
        public async Task<List<Flight>> GetAllFlightByAsync()
        {
            var fls = await _dbcontext.Flights.ToListAsync();
            return _mapper.Map<List<Flight>>(fls);
        }

        // get 1 id
        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            var fl = await _dbcontext.Flights.FindAsync(id);
            return _mapper.Map<Flight>(fl);
        }

        // get Page
        public async Task<PagedList<Flight>> GetFlightByPageAsync(int pageNumber, int pageSize)
        {
            var query = _dbcontext.Flights.AsQueryable();

            var pagedList = await PagedList<Flight>.CreateAsync((IQueryable<Flight>)query, pageNumber, pageSize);

            return pagedList;
        }

        public async Task<PagedList<Flight>> FindFlightByPageAsync(int pageNumber, int pageSize, string searchString = "")
        {
            var query = _dbcontext.Flights.AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(g => g.FlightName.Contains(searchString) || g.StartPoint.Contains(searchString));
            }

            var pagedList = await PagedList<Flight>.CreateAsync(query, pageNumber, pageSize);

            return pagedList;
        }

        public async Task<FlightModel> AddFlightAsync(FlightModel flightmodel, string userId)
        {
            var user = await _dbcontext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("Invalid userId");
            }



            //var newFL = _mapper.Map<Flight>(flightmodel);
            var newFL = new Flight
            {
                UserFlight = userId,
                FlightName = flightmodel.FlightName,
                EndPoint = flightmodel.EndPoint,
                CreatedFlight = flightmodel.CreatedFlight,
                StartPoint = flightmodel.StartPoint,

            };

            _dbcontext.Flights.Add(newFL);
            await _dbcontext.SaveChangesAsync();

            return flightmodel;
        }
        //update
        public async Task UpdateFlightAsync(int id, FlightModel flightmodel)
        {
            var existingProduct = await _dbcontext.Flights.FindAsync(flightmodel);

            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            if (id == flightmodel.FlightId)
            {
                var updateFl = _mapper.Map<Flight>(flightmodel);
                _dbcontext.Flights.Update(updateFl);
                await _dbcontext.SaveChangesAsync();
            }
        }

        // delete
        public async Task DeleteFlightAsync(int id)
        {
            var deleteFl = _dbcontext.Flights.SingleOrDefault(g => g.FlightId == id);
            if (deleteFl != null)
            {
                _dbcontext.Flights.Remove(deleteFl);
                await _dbcontext.SaveChangesAsync();
            }
        }
        public int GetFlightIdFromFlightName(string flightName)
        {
            var flight = _dbcontext.Flights.FirstOrDefault(f => f.FlightName == flightName);

            if (flight != null)
            {
                return flight.FlightId;
            }
            else
            {
                return 0;
            }
        }

    }
}
