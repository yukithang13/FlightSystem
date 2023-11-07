using FlightSystem.Helpers;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await accountServ.LoginAsync(model);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await accountServ.RegisterAsync(model);

            if (result is Response response)
            {
                if (response.Status == "Success")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Registration failed." });
        }

        /*        [Authorize(Roles = "Admin")]
        */
        [HttpPost("Register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await accountServ.RegisterAdminAsync(model);

            if (result is Response response)
            {
                if (response.Status == "Success")
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Admin registration failed." });
        }

    }
}
