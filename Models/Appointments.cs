namespace MentorShipProgram.Models
{
    public class Appointments
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<Mentor>? Mentors { get; set; }
        public ICollection<Categories>? Categories { get; set; }

    }
}
