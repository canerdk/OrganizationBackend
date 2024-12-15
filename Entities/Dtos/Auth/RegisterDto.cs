﻿using Entities.Common;

namespace Entities.Dtos.Auth
{
    public class RegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public short Type { get; set; }
    }
}
