using Entities.Common;

namespace Entities.Dtos.Auth
{
    public class TokenDto : IDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
