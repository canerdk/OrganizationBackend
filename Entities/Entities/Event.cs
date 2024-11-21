using Entities.Common;

namespace Entities.Entities
{
    public class Event : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int OrganizationId { get; set; }

        public required Organization Organization { get; set; }
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
