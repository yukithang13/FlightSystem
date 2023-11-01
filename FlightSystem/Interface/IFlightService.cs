using FlightSystem.Data;
using FlightSystem.Helpers;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IFlightService
    {

        Task<PagedList<Flight>> FindFlightByPageAsync(int pageNumber, int pageSize, string searchString = "");
        Task<List<Flight>> GetAllFlightByAsync();
        Task<Flight> GetFlightByIdAsync(int id);


        Task DeleteFlightAsync(int id);
        Task<PagedList<Flight>> GetFlightByPageAsync(int pageNumber, int pageSize);

        Task UpdateFlightAsync(int id, FlightModel flightmodel);

        Task<FlightModel> AddFlightAsync(FlightModel flightmodel, string userId);
    }
}
