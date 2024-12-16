using Entities.Common;
using Entities.Dtos.Participant;

namespace Entities.Dtos.Event
{
    public class EventDto : IDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int OrganizationId { get; set; }
        public List<ParticipantDto>? Participants { get; set; }
    }
}
