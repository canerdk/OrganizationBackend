using Entities.Common;

namespace Entities.Dtos.Participant
{
    public class ParticipantDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int EventId { get; set; }
    }
}
