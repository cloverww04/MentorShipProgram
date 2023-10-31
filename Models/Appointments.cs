namespace MentorShipProgram.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public string? DateTime { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public Mentor? Mentor { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public int MentorId { get; set; }
        public ICollection<MentorCategories>? MentorCategories { get; set; }

    }
}
