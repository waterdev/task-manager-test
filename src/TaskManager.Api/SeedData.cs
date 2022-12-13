using TaskManager.Domain;
using TaskManager.Infrastructure.Persistance;
using Task = TaskManager.Domain.Task;

namespace TaskManager.Api;

public static class SeedData
{
    private static readonly Guid TaskId = new Guid("762dd2ab-afb8-4187-92ed-1912d8aa2454");
    private static readonly Guid TaskListId = new Guid("3582b784-669e-49c9-bcf4-0d4b336e8003");
    private static readonly Guid OwnerId = new Guid("51fab9cc-ad6c-49eb-afc4-827ed11a7141");
    private static readonly Guid UserId = new Guid("dbe4dd38-54b7-4b9c-a831-a480c74bfd91");
    private static readonly Guid UserId2 = new Guid("5bab8aa3-e66c-421d-a2cf-02708d47b929");
    private static readonly Guid UserId3 = new Guid("5e747c0b-4c42-4ed3-9592-409105546e3a");


    private static readonly User User1 = new User
    {
        Id = UserId,
        Name = "User name"
    };
    private static readonly User User2 = new User
    {
        Id = UserId2,
        Name = "Second user"
    };
    private static readonly User User3 = new User
    {
        Id = UserId3,
        Name = "Third user"
    };
    
    private static readonly Task Task1 = new Task
    {
        Description = "do smth",
        TaskListId = TaskListId,
        Id = TaskId,
    };
    
    private static readonly TaskList TaskList1 = new TaskList
    {
        Name = "Todays Tasks",
        OwnerId = OwnerId,
        Id = TaskListId,
        CreatedAt = DateTime.Now
    };

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = serviceProvider.GetRequiredService<TaskManagerDbContext>())
        {
            if (dbContext.TaskLists.Any())
            {
                return;   // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(TaskManagerDbContext dbContext)
    {
        dbContext.Tasks.Add(Task1);
        dbContext.TaskLists.Add(TaskList1);
        dbContext.Users.Add(User1);
        dbContext.Users.Add(User2);
        dbContext.Users.Add(User3);
        dbContext.SaveChanges();
    }
}