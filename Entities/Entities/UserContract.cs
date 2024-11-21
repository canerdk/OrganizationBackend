using Entities.Common;

namespace Entities.Entities
{
    public class UserContract : BaseEntity
    {        
        public bool IsAccepted { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
        public required AppUser User { get; set; }
        public required Contract Contract { get; set; }
    }
}
