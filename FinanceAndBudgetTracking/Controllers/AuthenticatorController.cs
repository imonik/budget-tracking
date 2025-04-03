using System.Security.Claims;
using FinanceAndBudgetTracking.Services;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.DataLayer.Entities;

namespace FinanceAndBudgetTracking.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AuthService _auth;
        private readonly IAppUserRepository _appUserRepository;

        public AuthenticatorController(JwtService jtwService, AuthService auth, IAppUserRepository appUserRepository)
        {
            _jwtService = jtwService;
            _auth = auth;
            _appUserRepository = appUserRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Registration logic here
            var existingUser = await _appUserRepository.GetByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            _auth.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] saltdHash);

            var user = new AppUser
            {
                Name = model.Email,
                Email = model.Email,
                PasswordHash = passwordHash,
                SaltHash = saltdHash,
                CreatedOn = DateTime.UtcNow
            };

            var newUser = await _appUserRepository.RegisterAsync(user);
            var token = _jwtService.GenerateToken(model.Email, newUser.AppUserId.ToString());

            return Ok(new {token});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User already exists");
            }
            var existingUser = await _appUserRepository.GetByEmailAsync(model.Email);

            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            if (!_auth.VerifyPasswordHash(model.Password, existingUser.PasswordHash, existingUser.SaltHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim(ClaimTypes.NameIdentifier, existingUser.AppUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = _jwtService.GenerateToken(model.Email, existingUser.AppUserId.ToString());
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Store this token in a blacklist (Redis, DB, etc.)
            //_jwtService.AddToken(token, DateTime.UtcNow.AddHours(1)); // use token's exp time

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
