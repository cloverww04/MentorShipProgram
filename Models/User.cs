namespace MentorShipProgram.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }
    }
}
