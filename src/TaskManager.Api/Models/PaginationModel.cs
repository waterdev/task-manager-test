namespace TaskManager.Api.Models;

public class PaginationModel
{
    public int Take { get; init; } = 10;

    public int Skip { get; init; } = 0;
}