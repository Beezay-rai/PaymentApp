using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using PaymentApp.Data;
using PaymentApp.Model;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<AppUserDetail> _userManager;
        private readonly SignInManager<AppUserDetail> _siginManager;
        private readonly IOptions<AdminConfig>_adminConfig;
        private readonly IConfiguration _config;

        public AuthenticateController(IConfiguration config, SignInManager<AppUserDetail> siginManager,UserManager<AppUserDetail> userManager,IOptions<AdminConfig> adminConfig )
        {
            _siginManager = siginManager;
            _userManager = userManager;
            _adminConfig = adminConfig;
            _config= config;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var responsemodel = new ResponseModel();
            try
            {
                var check = await _userManager.FindByEmailAsync(model.Username);
                if (check != null)
                {
                    var singInResult = await _siginManager.PasswordSignInAsync(check, model.Password, false, false);
                    if (singInResult.Succeeded)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier,model.Username),
                            new Claim(ClaimTypes.Role,check.Role)
                        };
                        var jwtTokenHandler = new JwtSecurityTokenHandler();
                        var token = new JwtSecurityToken(
                            claims: claims,
                            audience: _config.GetSection("JWTConfig:ValidAudience").Value,
                            issuer: _config.GetSection("JWTConfig:ValidIssuer").Value,
                            expires: DateTime.UtcNow.AddDays(1),
                            notBefore: DateTime.UtcNow,
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWTConfig:IssuerSigningKey").Value)), SecurityAlgorithms.HmacSha256)
                            );
                        var response = new LoginResponse()
                        {
                            Expires = DateTime.UtcNow.AddDays(1),
                            NotBefore = DateTime.UtcNow,
                            Token = jwtTokenHandler.WriteToken(token),
                            Username = check.UserName,
                            Role = check.Role,
                            Email = model.Username,
                        };
                        return Ok(response);
                    }
                }
                responsemodel.Message = "Username Not Found !";
                responsemodel.Status = false;
                return Ok(responsemodel);
            }
            catch(Exception ex)
            {
                return Ok(new ResponseModel() { Status = false, Message = ex.Message,Data = $"Error-Datetime ={DateTime.UtcNow} " });
            }
        }
        [HttpGet]

        public async Task<IActionResult> Signout()
        {
            await _siginManager.SignOutAsync();
            return Ok();
        }

    }

    public class LoginModel
    {
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    public class LoginResponse
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Email { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DateTime Expires { get; set; }
        public DateTime NotBefore { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Role { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Token { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }






    public class SignUpModel
    {
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Email { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
