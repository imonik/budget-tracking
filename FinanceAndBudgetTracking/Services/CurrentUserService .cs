namespace FinanceAndBudgetTracking.API.Services
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using System.IdentityModel.Tokens.Jwt;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int? GetUserId()
        {
            var claim = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
                     ?? _context.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub);

            return int.TryParse(claim?.Value, out var id) ? id : null;
        }
    }

}
