namespace MentorShipProgram.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Mentor>? Mentors { get; set; }
        public ICollection<Categories>? Categories { get; set; }
        public ICollection<User>? Users { get; set; }
        public int UserId { get; set; }
        public int MentorId { get; set; }
        public int CategoryId { get; set;}

    }
}
