using Entities.Common;

namespace Entities.Entities
{
    public class Organization : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? OwnerFullName { get; set; }

    }
}
