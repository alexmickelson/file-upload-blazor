namespace web.Models;

public record User
{
    public required string Name { get; init; }
    public IEnumerable<string> OwnedFiles { get; init; } = [];
}