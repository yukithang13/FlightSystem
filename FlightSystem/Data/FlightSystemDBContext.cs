using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightSystem.Data
{
    public class FlightSystemDBContext : IdentityDbContext<User>
    {

        public FlightSystemDBContext(DbContextOptions<FlightSystemDBContext> opt) : base(opt)
        {

        }


        public virtual DbSet<GroupRole> GroupRoles { get; set; }
        public virtual DbSet<DocumentInfo> DocumentInfos { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<GroupInfo> GroupInfo { get; set; }

        public virtual DbSet<Group> Groups { get; set; }


    }
}
