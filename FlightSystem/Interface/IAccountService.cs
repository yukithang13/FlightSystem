using FlightSystem.Model;
using Microsoft.AspNetCore.Identity;

namespace FlightSystem.Interface
{
    public interface IAccountService
    {
        public Task<IdentityResult> RegisterAsync(RegisterModel model);
    }
}
