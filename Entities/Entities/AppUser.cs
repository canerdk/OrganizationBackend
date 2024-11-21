using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsDeleted { get; set; }
        public short Type { get; set; }
    }
}
