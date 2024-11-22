using Entities.Common;

namespace Entities.Dtos.UserContract
{
    public class UserContractDto : IDto
    {
        public int Id { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
    }
}
