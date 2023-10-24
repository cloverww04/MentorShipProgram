﻿namespace MentorShipProgram.Models
{
    public class Mentor
    {
        public int MentorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
