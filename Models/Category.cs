namespace MentorShipProgram.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<MentorCategories> MentorCategories { get; set; }

    }
}
