namespace web.Models;

public record MyUser
{
    public required int Id {get; init;}
    public required string Name { get; init; }
    // public IEnumerable<string> OwnedFiles { get; init; } = [];
}