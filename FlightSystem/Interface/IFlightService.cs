using FlightSystem.Data;
using FlightSystem.Helpers;
using FlightSystem.Model;

namespace FlightSystem.Interface
{
    public interface IFlightService
    {

        Task<PagedList<Flight>> FindFlightByPageAsync(int pageNumber, int pageSize, string searchString = "");
        Task<List<FlightModel>> GetAllFlightByAsync();
        Task<FlightModel> GetFlightByIdAsync(int id);
        Task<int> AddFlightAsync(FlightModel flightmodel);
        Task UpdateFlightAsync(int id, FlightModel flightmodel);
        Task DeleteFlightAsync(int id);
        Task<PagedList<Flight>> GetFlightByPageAsync(int pageNumber, int pageSize);
    }
}
