using TaskBlaster.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskBlaster.Data;

public class TaskBlasterDbContext : DbContext
{
    public TaskBlasterDbContext(DbContextOptions<TaskBlasterDbContext> options)
        : base(options) { }

    public DbSet<Duty> Duties { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
        .HasOne(c => c.User)
        .WithMany(u => u.Categories)
        .HasForeignKey(c => c.Uid)
        .HasPrincipalKey(u => u.Uid);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Uid = "a" },
            new User { Id = 2, Uid = "b" }
        );

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Uid = "a", Title = "Work" },
            new Category { Id = 2, Uid = "a", Title = "Home" },
            new Category { Id = 3, Uid = "a", Title = "Personal Goals" },
            new Category { Id = 4, Uid = "a", Title = "Errands" }
        );

        // Seed Resources
        modelBuilder.Entity<Resource>().HasData(
            new Resource { Id = 1, Title = "Laptop", Uid = "a" },
            new Resource { Id = 2, Title = "Vacuum Cleaner", Uid = "a" },
            new Resource { Id = 3, Title = "Notebook", Uid = "a" },
            new Resource { Id = 4, Title = "Car", Uid = "a" }
        );

        // Seed Duties
        modelBuilder.Entity<Duty>().HasData(
            new Duty
            {
                Id = 1,
                Title = "Submit monthly report",
                Description = "Gather all department data and submit the final report to the manager.",
                IsCompleted = false,
                CategoryId = 1,
                Priority = "High",
                Uid = "a"
            },
            new Duty
            {
                Id = 2,
                Title = "Clean the garage",
                Description = "Organize tools and dispose of unwanted items.",
                IsCompleted = false,
                CategoryId = 2,
                Priority = "Medium",
                Uid = "a"
            },
            new Duty
            {
                Id = 3,
                Title = "Run 5K",
                Description = "Jog around the neighborhood for cardio.",
                IsCompleted = true,
                CategoryId = 3,
                Priority = "Low",
                Uid = "a"
            }
        );

        // Seed Comments
        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = 1, DutyId = 1, Content = "Waiting on data from finance team." },
            new Comment { Id = 2, DutyId = 2, Content = "Don't forget to recycle old paint cans." },
            new Comment { Id = 3, DutyId = 3, Content = "Completed it in 28 minutes!" }
        );
    }

}