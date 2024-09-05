using System;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
