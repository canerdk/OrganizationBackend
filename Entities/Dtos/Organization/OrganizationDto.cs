using Entities.Common;

namespace Entities.Dtos.Organization
{
    public class OrganizationDto : IDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? OwnerFullName { get; set; }
    }
}
