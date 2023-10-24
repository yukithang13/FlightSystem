using FlightSystem.Data;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Identity;

namespace FlightSystem.Services
{
    public class AccountService : IAccountService
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IConfiguration configuration;
        private RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                FisrtNameUser = model.FirstName,
                LastNameUser = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync("User");

                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                await userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }
    }
}
