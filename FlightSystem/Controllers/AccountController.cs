using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountServ;
        public AccountController(IAccountService serv)
        {
            accountServ = serv;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> SignUp(RegisterModel signup)
        {
            var result = await accountServ.RegisterAsync(signup);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

    }
}
