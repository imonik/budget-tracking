using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.DataLayer.Interfaces;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.DataLayer.Entities;
using FinanceAndBudgetTracking.API.Services;

namespace FinanceAndBudgetTracking.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthService _auth;
        private readonly IAppUserRepository _appUserRepository;

        public AuthenticatorController(IJwtService jtwService, IAuthService auth, IAppUserRepository appUserRepository)
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
            var token = _jwtService.GenerateToken(newUser);

            return Ok(new {token});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request data");

            var user = await _appUserRepository.GetByEmailAsync(model.Email);
            if (user == null || !_auth.IsValidPassword(model.Password, user))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);
            var response = _auth.CreateLoginResponse(user, token);
            return Ok(response);
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
