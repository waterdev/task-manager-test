using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using Task = TaskManager.Domain.Task;

namespace TaskManager.Infrastructure.Persistance;

public class TaskManagerDbContext: DbContext
{
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase(databaseName: "TaskManagerDB");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskList>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<TaskList>()
            .HasMany(x => x.Tasks)
            .WithOne(x=> x.TaskList)
            .HasForeignKey(x=> x.TaskListId);

        modelBuilder.Entity<TaskList>()
            .Property(x => x.Id)
            .IsRequired();

        modelBuilder.Entity<Task>()
            .HasKey(x => x.Id);
       
    }
    
    public DbSet<TaskList> TaskLists { get; set; }
    
    public DbSet<Task> Tasks { get; set; }
    
    public DbSet<User> Users { get; set; }
}