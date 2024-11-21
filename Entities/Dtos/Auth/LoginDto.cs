using Entities.Common;

namespace Entities.Dtos.Auth
{
    public class LoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
