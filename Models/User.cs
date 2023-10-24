namespace MentorShipProgram.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FireName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public Mentor? Mentor { get; set; }
    }
}
