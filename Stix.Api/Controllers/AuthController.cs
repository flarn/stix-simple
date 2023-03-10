using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stix.Api.Auth;
using Stix.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Stix.Api.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this._roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUserRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(registerUserRequest.EmailAddress);
            if (existingUser != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ObjectResult(new ErrorResult("User already exists"));
            }

            ApplicationUser user = new()
            {
                Email = registerUserRequest.EmailAddress,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUserRequest.EmailAddress,
            };

            var result = await _userManager.CreateAsync(user, registerUserRequest.Password);
            if (!result.Succeeded)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ObjectResult(new ErrorResult(result.Errors.FirstOrDefault()?.Description ?? "Unable to create user"));
            }

            return Ok();
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login([FromBody] Login loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.EmailAddress);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires_in = token.ValidTo,
                    token_type = "Bearer",
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("set-role")]
        public async Task<IActionResult> SetRole([FromBody] SetRole setRoleRequest)
        {
            var user = await _userManager.FindByNameAsync(setRoleRequest.EmailAddress);
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new ObjectResult(new ErrorResult("User not found"));
            }

            if (!await _roleManager.RoleExistsAsync(setRoleRequest.Role))
                await _roleManager.CreateAsync(new ApplicationRole(setRoleRequest.Role));

            await _userManager.AddToRoleAsync(user, setRoleRequest.Role);

            return Ok();
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSigningKey"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudiance"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
