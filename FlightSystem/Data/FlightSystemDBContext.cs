using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Data
{
    public class FlightSystemDBContext : IdentityDbContext<User>
    {
        public FlightSystemDBContext(DbContextOptions<FlightSystemDBContext> opt) : base(opt)
        {

        }




    }
}
