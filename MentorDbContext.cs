using MentorShipProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorShipProgram
{
    public class MentorDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Mentor>? Mentors { get; set; }
        public DbSet<Appointments>? Appointments { get; set; }
        public DbSet<Categories>? Categories { get; set; }
        public DbSet<MentorCategories>? MentorCategories { get; set; }

        public MentorDbContext(DbContextOptions<MentorDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
