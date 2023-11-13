using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Interface
{
    public interface IAccountService
    {
        Task<bool> EditUserRoleAsync(string userId, string newRole);
        Task<object?> LoginAsync([FromBody] LoginModel model);
        Task<object?> RegisterAdminAsync([FromBody] RegisterModel model);
        Task<object?> RegisterAsync([FromBody] RegisterModel model);

    }
}
