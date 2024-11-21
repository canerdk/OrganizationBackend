using Entities.Common;

namespace Entities.Entities
{
    public class Participant : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
