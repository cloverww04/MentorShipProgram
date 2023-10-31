namespace MentorShipProgram.DTOs
{
    public class AppointmentsDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public UserDTO? User { get; set; }
        public MentorDTO? Mentor { get; set; }
    }
}
