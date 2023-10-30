using Microsoft.AspNetCore.Identity;

namespace FlightSystem.Data
{
    public class User : IdentityUser
    {
        public string Permission { get; set; }


    }
}
