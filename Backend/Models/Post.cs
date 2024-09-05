using System;

namespace Backend.Models
{
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
