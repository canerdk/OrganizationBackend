using Microsoft.AspNetCore.Http;

namespace DataAccess.Utilities.Auth
{
    public class LoginUserManager : ILoginUserManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetLoginUserId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserId")?.Value;
            if (userId != null)
                return int.Parse(userId);
            else
                return 0;
        }
    }
}
