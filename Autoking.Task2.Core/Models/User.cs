using System;
using System.Collections.Generic;

#nullable disable

namespace Autoking.Task2.Core.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Refleshtoken { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
