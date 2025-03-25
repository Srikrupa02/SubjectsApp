using Microsoft.EntityFrameworkCore;
using TopicManager.Models;

namespace TopicManager.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<Subject> Subjects { get; set; }
            public DbSet<Subtopic> Subtopics { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Define relationships
                modelBuilder.Entity<Subtopic>()
                    .HasOne(s => s.Subject)
                    .WithMany(s => s.Subtopics)
                    .HasForeignKey(s => s.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Seed initial data (Optional)
                modelBuilder.Entity<Subject>().HasData(
                    new Subject { Id = 1, Name = "Mathematics" },
                    new Subject { Id = 2, Name = "Physics" }
                );

                modelBuilder.Entity<Subtopic>().HasData(
                    new Subtopic { Id = 1, Name = "Algebra", SubjectId = 1 },
                    new Subtopic { Id = 2, Name = "Geometry", SubjectId = 1 },
                    new Subtopic { Id = 3, Name = "Mechanics", SubjectId = 2 }
                );
            }
        }
    }
