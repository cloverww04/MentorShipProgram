namespace MentorShipProgram.DTOs
{
    public class MentorDTO
    {
        public int? MentorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public List<CategoryDTO>? Categories { get; set; }
    }
}
