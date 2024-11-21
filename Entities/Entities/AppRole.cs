using Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public bool IsDeleted { get; set; }
        public string? TenantId { get; set; }
    }
}
