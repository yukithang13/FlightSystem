using Microsoft.AspNetCore.Identity;

namespace FlightSystem.Data
{
    public class User : IdentityUser
    {
        public string FisrtNameUser { get; set; } = null!;
        public string LastNameUser { get; set; } = null!;

    }
}
