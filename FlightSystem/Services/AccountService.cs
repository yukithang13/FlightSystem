using FlightSystem.Data;
using FlightSystem.Helpers;
using FlightSystem.Interface;
using FlightSystem.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public async Task<object?> LoginAsync(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id)
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }

            return null;
        }

        public async Task<object?> RegisterAsync(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new Response { Status = "Error", Message = "User already exists!" };
            }

            var user = new User
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Permission = model.Permission
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }

        public async Task<object?> RegisterAdminAsync(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new Response { Status = "Error", Message = "User already exists!" };
            }

            var user = new User
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Permission = model.Permission
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }

            var roles = new List<string> { UserRoles.Admin, UserRoles.User, UserRoles.Crew, UserRoles.Pilot };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRoleAsync(user, role);
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public async Task<bool> EditUserRoleAsync(string userId, string newRole)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            var role = await _roleManager.FindByNameAsync(newRole);
            if (role == null)
            {
                role = new IdentityRole(newRole);
                await _roleManager.CreateAsync(role);
            }

            await userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());

            await userManager.AddToRoleAsync(user, newRole);

            return true;
        }
    }
}
