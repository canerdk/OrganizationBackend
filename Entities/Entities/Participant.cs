using Entities.Common;

namespace Entities.Entities
{
    public class Participant : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int OrganizationId { get; set; }
        public required Organization Organization { get; set; }
    }
}
