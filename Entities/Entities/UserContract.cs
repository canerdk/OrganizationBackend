﻿using Entities.Common;

namespace Entities.Entities
{
    public class UserContract : BaseEntity
    {        
        public bool IsAccepted { get; set; }
        public DateTime Date { get; set; }
        public Guid? SessionId { get; set; }
        public int UserId { get; set; }
        public int ContractId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public AppUser User { get; set; }
        public Contract Contract { get; set; }
    }
}
