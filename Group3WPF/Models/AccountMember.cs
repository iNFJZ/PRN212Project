using System;
using System.Collections.Generic;

namespace Group3WPF.Models
{
    public partial class AccountMember
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
