using Entities.Common;

namespace Entities.Dtos.Auth
{
    public class ChangePasswordDto : IDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }
    }
}
