using MentorShipProgram.Models;
using Microsoft.EntityFrameworkCore;

namespace MentorShipProgram
{
    public class MentorDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Mentor>? Mentors { get; set; }
        public DbSet<Appointments>? Appointments { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<MentorCategories>? MentorCategories { get; set; }

        public MentorDbContext(DbContextOptions<MentorDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed data with mentors
            modelBuilder.Entity<Mentor>().HasData(new Mentor[]
             {
                new Mentor { MentorId = 1, FirstName = "Adonis", LastName = "Bridges", Bio = "I am a Product Experience Manager with extensive experience in the corporate world." },
                new Mentor { MentorId = 2, FirstName = "Michael", LastName = "Perso", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                new Mentor { MentorId = 3, FirstName = "Kai", LastName = "Okonko", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                new Mentor { MentorId = 4, FirstName = "Bri", LastName = "Karter", Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." }

             });

            //seed data with categories
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { CategoryId = 1, CategoryName = "Communications"},
                new Category { CategoryId = 2, CategoryName = "Professional Development"},
                new Category { CategoryId = 3, CategoryName = "Networking"},
                new Category { CategoryId = 4, CategoryName = "Leadership"},
                new Category { CategoryId = 5, CategoryName = "Career and Education Planning"}
            });

            //seed data with user 
            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { UserId = 1, FirstName = "Pam", LastName = "Carson", MentorId = 1, Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore."},
                new User { UserId = 2, FirstName = "Austin", LastName = "Barter", MentorId = 2, Bio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore."},
            });

            //seed data with appointments
            modelBuilder.Entity<Appointments>().HasData(new Appointments[]
            {
                new Appointments { Id = 1, UserId = 1, MentorId = 1, DateTime = "2023-11-02T10:00"},
                new Appointments { Id = 2, UserId = 2, MentorId = 2, DateTime = "2023-10-31T13:00"},

            });

            modelBuilder.Entity<MentorCategories>()
                .HasKey(mc => new { mc.MentorId, mc.CategoryId });

            modelBuilder.Entity<MentorCategories>()
                .HasOne(mc => mc.Mentor)
                .WithMany(mentor => mentor.MentorCategories)
                .HasForeignKey(mc => mc.MentorId);

            modelBuilder.Entity<MentorCategories>()
                .HasOne(mc => mc.Categories)
                .WithMany(category => category.MentorCategories)
                .HasForeignKey(mc => mc.CategoryId);

        }

    }
}
