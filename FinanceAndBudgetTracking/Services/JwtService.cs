namespace FinanceAndBudgetTracking.Services
{
    using FinanceAndBudgetTracking.API.Services;
    using FinanceAndBudgetTracking.DataLayer.Entities;
    using FinanceAndBudgetTracking.Models.DTO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class JwtService: IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;


        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JwtSettings:Secret"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
        }

        public string GenerateToken(AppUser user) 
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.AppUserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("AppUser", "true"),  // for AppUserPolicy
                new Claim("AdminUser", "true") // for AdminUserPolicy
            };

            var identity = new ClaimsIdentity(claims);
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = creds,
                Issuer = _issuer,
                Audience = _audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
