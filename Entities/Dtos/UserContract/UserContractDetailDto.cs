using Entities.Common;
using Entities.Dtos.Auth;
using Entities.Dtos.Contract;
using Entities.Dtos.Event;

namespace Entities.Dtos.UserContract
{
    public class UserContractDetailDto : IDto
    {
        public int Id { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime Date { get; set; }
        public Guid? SessionId { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
        public int EventId { get; set; }
        public UserDto User { get; set; }
        public ContractDto Contract { get; set; }
        public EventDto Event { get; set; }
    }
}
