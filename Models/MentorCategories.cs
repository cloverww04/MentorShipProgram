namespace MentorShipProgram.Models
{
    public class MentorCategories
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Categories? Categories { get; set; }
        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }
    }
}
